using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using RestaurantPosApp.Contracts;
using RestaurantPosApp.Models;

namespace RestaurantPosApp.Controllers
{
    public class OwnerController : Controller
    {
        private readonly ILogger<OwnerController> _logger;
        private readonly IRepositoryWrapper _repo;
        private readonly IEmailService _emailService;


        public OwnerController(ILogger<OwnerController> logger, IRepositoryWrapper repo, IEmailService emailService)
        {
            _logger = logger;
            _repo = repo;
            _emailService = emailService;
        }

        public async Task<IActionResult> Index()
        {
            //await _emailService.EmailAsync(new Owner { EmailAddress = "charliesather18@gmail.com", Name = "Charlie Sather"}, "<h1>Hello!</h1>");

            ViewBag.Categories = await Task.Run(() => _repo.MenuCategory.GetMenuCategories());
            return View();
        }
        public IActionResult CreateMenuCategory() => View();

        [HttpPost]
        public IActionResult CreateMenuCategory(MenuCategory menuCategory)
        {
            if (ModelState.IsValid)
            {
                if (!_repo.MenuCategory.MenuCategoryExists(menuCategory.CategoryName))
                {
                    _repo.MenuCategory.CreateMenuCategory(menuCategory);
                    _repo.Save();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(menuCategory);
        }

        public IActionResult AddOrEditMenuItem(int id = 0)
        {
            var model = new MenuItemViewModel();

            if (id != 0)
            {
                //build model from Db
            }

            ViewBag.MenuCategories = new SelectList(_repo.MenuCategory.GetMenuCategories(), "MenuCategoryId", "CategoryName");
            ViewBag.Ingredients = new SelectList(_repo.Ingredient.GetIngredients(), "IngredientId", "Name");


            return View(model);
        }

        [HttpPost]
        public IActionResult AddOrEditMenuItem(MenuItemViewModel menuItemViewModel)
        {
            if (ModelState.IsValid)
            {
                if (menuItemViewModel.MenuItem.MenuItemId == 0)
                {
                    _repo.MenuItem.CreateMenuItem(menuItemViewModel.MenuItem);
                    _repo.Save();

                    var ids = menuItemViewModel.Recipe.Select(x => x.IngredientId).Distinct().AsEnumerable();
                    var ingredients = _repo.Ingredient.GetIngredients(ids).ToList();

                    foreach (var item in menuItemViewModel.Recipe)
                    {
                        var ingredient = ingredients.Single(x => x.IngredientId == item.IngredientId);
                        item.MenuItemId = menuItemViewModel.MenuItem.MenuItemId;
                        //item.Cost = ((decimal)item.Quantity / ingredient.BaseUnitOfWeight) * ingredient.PricePerUnit;
                        item.Cost = CalculateCost(item.Quantity, ingredient.BaseUnitOfWeight, ingredient.PricePerUnit);
                    }

                    _repo.MenuItemIngredient.AddListOfMenuItemIngredients(menuItemViewModel.Recipe);
                    _repo.Save();
                }
                else
                {

                    //_repo.MenuItemIngredient.UpdateMenuItemIngredient();
                }

                return RedirectToAction(nameof(Index));
            }
            ViewBag.MenuCategories = new SelectList(_repo.MenuCategory.GetMenuCategories(), "MenuCategoryId", "CategoryName");
            ViewBag.Ingredients = new SelectList(_repo.Ingredient.GetIngredients(), "IngredientId", "Name");
            return View(menuItemViewModel);
        }
        public decimal CalculateCost(int quantity, int unit, decimal pricePerUnit)
        {
            return ((decimal) quantity / unit) * pricePerUnit;
        }

        [HttpPost]
        public IActionResult CreateOrder(PlacedOrder placedOrder)
        {
            if (ModelState.IsValid)
            {
                var menuItems = _repo.MenuItem.GetMenuItemsByIds(placedOrder.OrderedItems.Select(x => x.MenuItemId).Distinct()).ToList();
                var orderResult = InventoryCanMakeOrder(placedOrder, menuItems);
                if (orderResult.Item1)
                {
                    placedOrder.OrderedTimestamp = DateTime.Now;
                    //placedOrder.UserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    UpdateInventory(placedOrder, orderResult.Item3);
                    _repo.PlacedOrder.CreateOrder(placedOrder);
                    _repo.Save();
                }
                else
                {
                    var menuItem = menuItems.Find(x => x.Recipe.Exists(x => x.IngredientId == orderResult.Item2));
                    var menuItemingredient = menuItem.Recipe.Find(x => x.IngredientId == orderResult.Item2);
                    var errorMessage = $"Sorry, we Can't make {menuItem.Name} as we have no more {menuItemingredient.Ingredient.Name}";
                    return Json(new { ErrorMessage = errorMessage });
                }
                return RedirectToAction(nameof(Index));
            }
            return Json(placedOrder);
        }

        public (bool, int?, Dictionary<int, int>) InventoryCanMakeOrder(PlacedOrder placedOrder, IEnumerable<MenuItem> menuItems)
        {
            // (can we make it?, ingredientId we're out of, ingredientSums dictionary)
            (bool, int?, Dictionary<int, int>) canMake = (true, null, new Dictionary<int, int>());
            var groupedMenuItems = placedOrder.OrderedItems.GroupBy(x => x.MenuItemId).ToList();

            //Key is ingredientId 
            //Value is total amount needed of that ingredient to make the order 
            var ingredientSums = menuItems.SelectMany(x => x.Recipe, (x, ingredient) => ingredient.IngredientId).Distinct().ToDictionary(x => x, x => 0);
            var ingredientSumsFromInventory = new Dictionary<int, int>(ingredientSums);

            canMake.Item3 = ingredientSums;
            //build up amount of ingredients in inventory
            ingredientSumsFromInventory.Keys.ToList().ForEach(key => ingredientSumsFromInventory[key] = _repo.InventoryItem.GetSumForIngredient(key));

            // Big-O scary
            foreach (var items in groupedMenuItems)
            {
                foreach (var item in items)
                {
                    var menuItem = menuItems.Single(x => x.MenuItemId == item.MenuItemId);
                    foreach (var ingredient in menuItem.Recipe)
                    {
                        ingredientSums[ingredient.IngredientId] += ingredient.Quantity * item.Quantity;
                    };
                }
            }


            foreach (var key in ingredientSums.Keys)
            {
                if (ingredientSums[key] > ingredientSumsFromInventory[key])
                {
                    canMake.Item1 = false;
                    canMake.Item2 = key;
                    break;
                }
            }

            return canMake;
        }
        public void UpdateInventory(PlacedOrder placedOrder, Dictionary<int, int> ingredientSums)
        {
            var inventoryItemsFromDb = _repo.InventoryItem.GetInventoryItemsByIngredientList(ingredientSums.Keys.ToList()).ToList();
            foreach (var inventoryItem in inventoryItemsFromDb)
            {
                var amountNeeded = ingredientSums[inventoryItem.IngredientId];
                inventoryItem.BulkPrice -= CalculateCost(amountNeeded, inventoryItem.Ingredient.BaseUnitOfWeight,inventoryItem.Ingredient.PricePerUnit);
                inventoryItem.AmountInGrams -= amountNeeded;
                inventoryItem.IsLow = InventoryItemIsLow(inventoryItem);
            }
            _repo.InventoryItem.UpdateRangeOfInventoryItems(inventoryItemsFromDb);
            _repo.Save();
        }

        public IActionResult Statistics() => View();

        //public bool CanMakeMenuItem(MenuItem menuItem, int quantity)
        //{
        //    var recipeDictionary = menuItem.Recipe.ToDictionary(x => x.Quantity);

        //    return true;
        //}


        public IActionResult AddIngredients() => View();

        [HttpPost]
        public IActionResult AddIngredients(List<Ingredient> ingredients)
        {
            if (ModelState.IsValid)
            {
                _repo.Ingredient.AddRange(ingredients);
                _repo.Save();
            }
            else
            {
                return View(ingredients);
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult AddInventory()
        {
            ViewBag.Ingredients = new SelectList(_repo.Ingredient.GetIngredients(), "IngredientId", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult AddInventory(List<InventoryItem> inventoryItems)
        {
            if (ModelState.IsValid)
            {
                var ingredients = _repo.Ingredient.GetIngredients(inventoryItems.Select(x => x.IngredientId).Distinct().ToList()).ToList();
                var inventoryFromDb = _repo.InventoryItem.GetInventoryItemsByIngredientList(inventoryItems.Select(x => x.IngredientId).Distinct().ToList()).ToList();

                foreach (var inventoryItem in inventoryItems)
                {
                    var ingredient = ingredients.Single(x => x.IngredientId == inventoryItem.IngredientId);
                    if (inventoryFromDb.Any(x => x.IngredientId == inventoryItem.IngredientId))
                    {
                        var inventoryItemFromDb = inventoryFromDb.Find(x => x.IngredientId == inventoryItem.IngredientId);
                        inventoryItem.InventoryItemId = inventoryItemFromDb.InventoryItemId;
                        inventoryItem.AmountInGrams += inventoryItemFromDb.AmountInGrams;
                        inventoryItem.BulkPrice = inventoryItemFromDb.BulkPrice;
                    }
                    //inventoryItem.Ingredient = await Task.Run(() => _repo.Ingredient.GetIngredientAsNoTracking(inventoryItem.IngredientId));

                    //inventoryItem.BulkPrice += ((decimal)inventoryItem.AmountInGrams / ingredient.BaseUnitOfWeight) * ingredient.PricePerUnit;
                    inventoryItem.BulkPrice += CalculateCost(inventoryItem.AmountInGrams, ingredient.BaseUnitOfWeight, ingredient.PricePerUnit);
                    inventoryItem.IsLow = InventoryItemIsLow(inventoryItem);

                    //inventoryItem.Ingredient = null; //Need to make Ingredient null or ef core tries to insert it into db again which causes an exception since it has a PK already and will cause a collision
                }
                _repo.InventoryItem.UpdateRangeOfInventoryItems(inventoryItems);
                _repo.Save();
            }
            else
            {
                return View(inventoryItems);
            }
            return RedirectToAction(nameof(Index));
        }
        public bool InventoryItemIsLow(InventoryItem inventoryItem) => inventoryItem.AmountInGrams <= inventoryItem.LowerThreshold;

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
