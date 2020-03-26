using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using RestaurantPosApp.Contracts;
using RestaurantPosApp.Models;

namespace RestaurantPosApp.Controllers
{
    [Authorize(Roles ="Owner,Employee")]
    public class RestaurantController : Controller
    {
        private readonly ILogger<RestaurantController> _logger;
        private readonly IRepositoryWrapper _repo;

        public RestaurantController(ILogger<RestaurantController> logger, IRepositoryWrapper repo)
        {
            _logger = logger;
            _repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Categories = await Task.Run(() => _repo.MenuCategory.GetMenuCategories().ToList());
            return View();
        }
        
        [Authorize(Roles ="Owner")]
        public IActionResult Create()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var owner = _repo.Owner.GetOwner(userId);
            var restaurant = _repo.Restaurant.GetRestaurantByOwenerId(owner.OwnerId);

            if(restaurant == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        [Authorize(Roles = "Owner")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var owner = _repo.Owner.GetOwner(userId);
                restaurant.OwnerId = owner.OwnerId;
                _repo.Restaurant.CreateRestaurant(restaurant);
                _repo.Save();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(restaurant);
            }
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
                    placedOrder.UserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    UpdateInventory(placedOrder, orderResult.Item3);
                    _repo.PlacedOrder.CreateOrder(placedOrder);
                    _repo.Save();
                }
                else
                {
                    var menuItem = menuItems.Find(x => x.Recipe.Exists(x => x.IngredientId == orderResult.Item2));
                    var menuItemingredient = menuItem.Recipe.Find(x => x.IngredientId == orderResult.Item2);
                    var errorMessage = $"Sorry, we Can't make {menuItem.Name} as we have don't have enough {menuItemingredient.Ingredient.Name}";
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
