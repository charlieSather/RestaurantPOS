using RestaurantPosApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantPosApp.Contracts
{
    public interface IShoppingListIngredientRepository : IRepositoryBase<ShoppingListIngredient>
    {
        void CreateShoppingListIngredient(ShoppingListIngredient shoppingListIngredient);
        void UpdateShoppingListIngredient(ShoppingListIngredient shoppingListIngredient);
        void DeleteShoppingListIngredient(ShoppingListIngredient shoppingListIngredient);
        void AddRangeOfShoppingListIngredient(IEnumerable<ShoppingListIngredient> shoppingListIngredients);
        IEnumerable<ShoppingListIngredient> GetShoppingListIngredientsByShoppingListId(int shoppingListId);

    }
}
