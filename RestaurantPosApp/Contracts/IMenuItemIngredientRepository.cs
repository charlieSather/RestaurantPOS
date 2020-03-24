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
        void AddListOfMenuItemIngredients(List<MenuItemIngredient> menuItemIngredients);
        void UpdateListOfMenuItemIngredients(List<MenuItemIngredient> menuItemIngredients);
        MenuItemIngredient GetMenuItemIngredient(int id);
        MenuItemIngredient GetMenuItemIngredientByIngredientId(int ingredientId);
        IQueryable<MenuItemIngredient> GetMenuItemIngredientsByMenuItemId(int menuItemId);
        IEnumerable<Ingredient> GetIngredientsByMenuItemIds(List<int> menuItemIds);
    }
}
