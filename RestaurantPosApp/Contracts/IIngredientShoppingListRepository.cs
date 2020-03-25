using RestaurantPosApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantPosApp.Contracts
{
    public interface IIngredientShoppingListRepository
    {
        void CreateIngredientShoppingList(IngredientShoppingList ingredientShoppingList);
        void UpdateIngredientShoppingList(IngredientShoppingList ingredientShoppingList);
        void DeleteIngredientShoppingList(IngredientShoppingList ingredientShoppingList);
        IEnumerable<IngredientShoppingList> GetShoppingListIngredientsByShoppingListId(int shoppingListId);

    }
}
