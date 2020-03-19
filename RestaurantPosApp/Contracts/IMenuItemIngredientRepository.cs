using RestaurantPosApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantPosApp.Contracts
{
    public interface IMenuItemIngredientRepository : IRepositoryBase<MenuItemIngredient>
    {
        void CreateMenuItemIngredient(MenuItemIngredient menuItemIngredient);
        void UpdateMenuItemIngredient(MenuItemIngredient menuItemIngredient);
        void DeleteMenuItemIngredient(MenuItemIngredient menuItemIngredient);
        MenuItemIngredient GetMenuItemIngredient(int id);
        MenuItemIngredient GetMenuItemIngredientByIngredientId(int ingredientId);
        IQueryable<MenuItemIngredient> GetMenuItemIngredientsByMenuItemId(int menuItemId);
    }
}
