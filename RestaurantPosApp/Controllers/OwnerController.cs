using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

        public OwnerController(ILogger<OwnerController> logger, IRepositoryWrapper repo)
        {
            _logger = logger;
            _repo = repo;
        }

        public async Task<IActionResult> Index()
        {
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
                        item.Cost = ((decimal) item.Quantity / ingredient.BaseUnitOfWeight) * ingredient.PricePerUnit;
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
        public void AddRecipe(List<MenuItemIngredient> menuItemIngredients)
        {
        }

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
