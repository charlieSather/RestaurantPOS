using Microsoft.EntityFrameworkCore;
using RestaurantPosApp.Contracts;
using RestaurantPosApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantPosApp.Data
{
    public class ShoppingListRepository : RepositoryBase<ShoppingList>, IShoppingListRepository
    {
        public ShoppingListRepository(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        {
        }

        public void CreateShoppingList(ShoppingList shoppingList) => Create(shoppingList);
        public void DeleteShoppingList(ShoppingList shoppingList) => Delete(shoppingList);
        public void UpdateShoppingList(ShoppingList shoppingList) => Update(shoppingList);
        public ShoppingList GetShoppingList(int id) =>
            FindByCondition(x => x.ShoppingListId == id)
                .Include(x => x.ShoppingItems)
                .ThenInclude(x => x.Ingredient)
                .FirstOrDefault();
        public IEnumerable<ShoppingList> GetShoppingListByStatus(bool status) => FindByCondition(x => x.IsCompleted == status).Include(x => x.ShoppingItems).ThenInclude(i => i.Ingredient);
        public IEnumerable<ShoppingList> GetShoppingListByOwnerId(string ownerId) => FindByCondition(x => x.OwnerId == ownerId).Include(x => x.ShoppingItems).ThenInclude(i => i.Ingredient);

    }
}
