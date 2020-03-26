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

        public void CreateShoppingListIngredient(ShoppingListIngredient shoppingListIngredient) => Create(shoppingListIngredient);
        public void DeleteShoppingListIngredient(ShoppingListIngredient shoppingListIngredient) => Delete(shoppingListIngredient);
        public void UpdateShoppingListIngredient(ShoppingListIngredient shoppingListIngredient) => Update(shoppingListIngredient);
        public IEnumerable<ShoppingListIngredient> GetShoppingListIngredientsByShoppingListId(int shoppingListId) => FindByCondition(x => x.ShoppingListId == shoppingListId).Include(i => i.Ingredient);
        public void AddRangeOfShoppingListIngredient(IEnumerable<ShoppingListIngredient> shoppingListIngredients) => AddRange(shoppingListIngredients);
    }
}
