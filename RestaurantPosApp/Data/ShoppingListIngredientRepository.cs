using Microsoft.EntityFrameworkCore;
using RestaurantPosApp.Contracts;
using RestaurantPosApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantPosApp.Data
{
    public class ShoppingListIngredientRepository : RepositoryBase<ShoppingListIngredient>, IShoppingListIngredientRepository
    {
        public ShoppingListIngredientRepository(ApplicationDbContext applicationDbContext)
            :base(applicationDbContext)
        {
        }

        public void CreateIngredientShoppingList(ShoppingListIngredient ingredientShoppingList) => Create(ingredientShoppingList);
        public void DeleteIngredientShoppingList(ShoppingListIngredient ingredientShoppingList) => Delete(ingredientShoppingList);
        public void UpdateIngredientShoppingList(ShoppingListIngredient ingredientShoppingList) => Update(ingredientShoppingList);
        public IEnumerable<ShoppingListIngredient> GetShoppingListIngredientsByShoppingListId(int shoppingListId) => FindByCondition(x => x.ShoppingListId == shoppingListId).Include(i => i.Ingredient);
    }
}
