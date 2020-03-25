using Microsoft.EntityFrameworkCore;
using RestaurantPosApp.Contracts;
using RestaurantPosApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantPosApp.Data
{
    public class IngredientShoppingListRepository : RepositoryBase<IngredientShoppingList>, IIngredientShoppingListRepository
    {
        public IngredientShoppingListRepository(ApplicationDbContext applicationDbContext)
            :base(applicationDbContext)
        {
        }

        public void CreateIngredientShoppingList(IngredientShoppingList ingredientShoppingList) => Create(ingredientShoppingList);
        public void DeleteIngredientShoppingList(IngredientShoppingList ingredientShoppingList) => Delete(ingredientShoppingList);
        public void UpdateIngredientShoppingList(IngredientShoppingList ingredientShoppingList) => Update(ingredientShoppingList);
        public IEnumerable<IngredientShoppingList> GetShoppingListIngredientsByShoppingListId(int shoppingListId) => FindByCondition(x => x.IngredientShoppingListId == shoppingListId).Include(i => i.Ingredient);
    }
}
