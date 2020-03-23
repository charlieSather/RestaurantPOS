using Microsoft.EntityFrameworkCore;
using RestaurantPosApp.Contracts;
using RestaurantPosApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantPosApp.Data
{
    public class MenuItemIngredientRepository : RepositoryBase<MenuItemIngredient>, IMenuItemIngredientRepository
    {
        public MenuItemIngredientRepository(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        {
        }

        public void CreateMenuItemIngredient(MenuItemIngredient menuItemIngredient) => Create(menuItemIngredient);

        public void DeleteMenuItemIngredient(MenuItemIngredient menuItemIngredient) => Delete(menuItemIngredient);
        public void UpdateMenuItemIngredient(MenuItemIngredient menuItemIngredient) => Update(menuItemIngredient);
        public void AddListOfMenuItemIngredients(List<MenuItemIngredient> menuItemIngredients)
        {
            //foreach (var menuItemIngredient in menuItemIngredients)
            //{
            //    menuItemIngredient.MenuItemId = menuItemId;
            //}
            AddRange(menuItemIngredients);
        }
        public void UpdateListOfMenuItemIngredients(List<MenuItemIngredient> menuItemIngredients)
        {
            foreach (var menuItemIngredient in menuItemIngredients)
            {
                Update(menuItemIngredient);
            }
        }

        public MenuItemIngredient GetMenuItemIngredient(int id) =>
            FindByCondition(x => x.MenuItemIngredientId == id)
                .Include(m => m.MenuItem)
                .Include(i => i.Ingredient)
                .FirstOrDefault();
        public MenuItemIngredient GetMenuItemIngredientByIngredientId(int ingredientId) =>
            FindByCondition(x => x.IngredientId == ingredientId)
                .Include(m => m.MenuItem)
                .Include(i => i.Ingredient)
                .FirstOrDefault();

        public IQueryable<MenuItemIngredient> GetMenuItemIngredientsByMenuItemId(int menuItemId) =>
            FindByCondition(x => x.MenuItemId == menuItemId)
                .Include(m => m.MenuItem)
                .Include(i => i.Ingredient);
    }
}
