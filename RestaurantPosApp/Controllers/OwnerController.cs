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
                var ids = menuItemViewModel.Recipe.Select(x => x.IngredientId).Distinct().AsEnumerable();
                var ingredients = _repo.Ingredient.GetIngredients(ids).ToList();

                if (menuItemViewModel.MenuItem.MenuItemId == 0)
                {
                    _repo.MenuItem.CreateMenuItem(menuItemViewModel.MenuItem);
                    _repo.Save();

                    foreach (var item in menuItemViewModel.Recipe)
                    {
                        var ingredient = ingredients.Single(x => x.IngredientId == item.IngredientId);
                        item.MenuItemId = menuItemViewModel.MenuItem.MenuItemId;
                        item.Cost = CalculateCost(item.Quantity, ingredient.BaseUnitOfWeight, ingredient.PricePerUnit);
                    }

                    _repo.MenuItemIngredient.AddListOfMenuItemIngredients(menuItemViewModel.Recipe);
                    _repo.Save();
                }
                else
                {
                    var menuItem = menuItemViewModel.MenuItem;
                    menuItem.Recipe = menuItemViewModel.Recipe;

                    foreach (var item in menuItem.Recipe)
                    {
                        var ingredient = ingredients.Single(x => x.IngredientId == item.IngredientId);
                        item.Cost = CalculateCost(item.Quantity, ingredient.BaseUnitOfWeight, ingredient.PricePerUnit);
                    }
                    _repo.MenuItem.UpdateMenuItem(menuItem);
                    _repo.Save();
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

        public IActionResult DeleteMenuItem(int id)
        {
            try
            {
                var menuItem = _repo.MenuItem.GetMenuItem(id);
                _repo.MenuItem.DeleteMenuItem(menuItem);
                _repo.Save();
            }
            catch(Exception ex)
            {
                return RedirectToAction(nameof(AddOrEditMenuItem), id);
            }
            return RedirectToAction(nameof(Index));
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

                    inventoryItem.BulkPrice += CalculateCost(inventoryItem.AmountInGrams, ingredient.BaseUnitOfWeight, ingredient.PricePerUnit);
                    inventoryItem.IsLow = InventoryItemIsLow(inventoryItem);
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

        [HttpGet]
        public async Task<IActionResult> GenerateShoppingList()
        {
            var lowInventoryItems = _repo.InventoryItem.GetLowInventoryItems().ToList();
            if(lowInventoryItems.Count == 0)
            {
                return Json(new { Message = "No low inventory items, no shopping list was created." });
            }

            var shoppingList = new ShoppingList();
            shoppingList.OwnerId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            shoppingList.ShoppingItems = new List<ShoppingListIngredient>();

            _repo.ShoppingList.CreateShoppingList(shoppingList);
            _repo.Save();

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

            _repo.ShoppingListIngredient.AddRangeOfShoppingListIngredient(shoppingList.ShoppingItems);
            _repo.Save();

            var owner = _repo.Owner.GetOwner(this.User.FindFirstValue(ClaimTypes.NameIdentifier));

            //await _emailService.EmailAsync(owner, htmlString);
            return Json(new { Message = "Successfully generated and emailed the shopping list!"});
        }

        [HttpGet]
        public IActionResult ConfirmShoppingLists()
        {
            var model = _repo.ShoppingList.GetShoppingListByStatus(false);
            return View(model);
        }

        public IActionResult ConfirmShoppingList(int id)
        {
            var shoppingList = _repo.ShoppingList.GetShoppingList(id);
            shoppingList.IsCompleted = true;

            var ingredientIds = shoppingList.ShoppingItems.Select(x => x.IngredientId).ToList();
            var inventoryItems = _repo.InventoryItem.GetInventoryItemsByIngredientList(ingredientIds).ToList();

            foreach (var item in inventoryItems)
            {
                var shoppingListItem = shoppingList.ShoppingItems.Find(x => x.IngredientId == item.IngredientId);
                item.AmountInGrams += shoppingListItem.AmountInGrams;
                item.BulkPrice = CalculateCost(item.AmountInGrams, shoppingListItem.Ingredient.BaseUnitOfWeight, shoppingListItem.Ingredient.PricePerUnit);
                item.IsLow = InventoryItemIsLow(item);
            }

            _repo.InventoryItem.UpdateRangeOfInventoryItems(inventoryItems);
            _repo.Save();


            shoppingList.ShoppingItems = null;
            _repo.ShoppingList.UpdateShoppingList(shoppingList);
            _repo.Save();

            return RedirectToAction(nameof(ConfirmShoppingLists));
        }

        [HttpGet]
        public IActionResult GetTopSellingMenuItemsByDateRange(DateTime start, DateTime end)
        {
            var model = new StatisticsViewModel();
            var grabTopTen = 10;

            var result = _repo.OrderMenuItem.GetTopSellingMenuItemsByDate(start, end, grabTopTen).ToList();

            model.TopSellingMenuItems = result;
            return PartialView("_StatisticsFilter",model);
        }

    }
}