using RestaurantPosApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantPosApp.Contracts
{
    public interface IShoppingListIngredientRepository : IRepositoryBase<ShoppingListIngredient>
    {
        void CreateIngredientShoppingList(ShoppingListIngredient ingredientShoppingList);
        void UpdateIngredientShoppingList(ShoppingListIngredient ingredientShoppingList);
        void DeleteIngredientShoppingList(ShoppingListIngredient ingredientShoppingList);
        IEnumerable<ShoppingListIngredient> GetShoppingListIngredientsByShoppingListId(int shoppingListId);

    }
}
