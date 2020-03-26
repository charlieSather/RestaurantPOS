using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestaurantPosApp.Contracts;
using RestaurantPosApp.Models;

namespace RestaurantPosApp.Controllers
{
    [Authorize(Roles = "Owner")]
    public class OwnerController : Controller
    {
        private readonly IRepositoryWrapper _repo;
        private readonly IEmailService _emailService;
        public OwnerController(IRepositoryWrapper repo, IEmailService emailService)
        {
            _repo = repo;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var owner = _repo.Owner.GetOwner(userId);

            if(owner != null)
            {
                return RedirectToAction("Index", "Restaurant");
            }
            return RedirectToAction(nameof(Create));
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Owner owner)
        {
            if (ModelState.IsValid)
            {
                var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                owner.UserId = userId;
                _repo.Owner.CreateOwner(owner);
                _repo.Save();

                //system will be limited to a single restuarant and owner for now
                return RedirectToAction("Create", "Restaurant");
            }
            else
            {
                return View(owner);
            }
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
                var menuItem = _repo.MenuItem.GetMenuItem(id);
                model.MenuItem = menuItem;
                model.Recipe = menuItem.Recipe;
                return View(model);
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
            return ((decimal)quantity / unit) * pricePerUnit;
        }

        public IActionResult Statistics() => View();

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

        public async Task<IActionResult> GenerateShoppingList()
        {
            var shoppingList = new ShoppingList();
            //shoppingList.OwnerId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            shoppingList.ShoppingItems = new List<ShoppingListIngredient>();


            var lowInventoryItems = _repo.InventoryItem.GetLowInventoryItems().ToList();
            var htmlString = "<ul>";
            foreach (var item in lowInventoryItems)
            {
                var recommendedAmount = item.LowerThreshold * 10;
                shoppingList.ShoppingItems.Add(
                    new ShoppingListIngredient
                    {
                        IngredientId = item.IngredientId,
                        AmountInGrams = recommendedAmount,
                        ShoppingListId = shoppingList.ShoppingListId
                    }
                );
                htmlString += $"<li>{item.Ingredient.Name}: {recommendedAmount} grams.</li>";
            }
            htmlString += "</ul>";

            //await _emailService.EmailAsync(new Owner { EmailAddress = "", Name = "CSather"}, htmlString);
            return View("Statistics");
        }
        public void InputShoppingList(int id)
        {

        }

    }
}