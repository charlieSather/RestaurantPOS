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
        public IEnumerable<ShoppingList> GetShoppingListByStatus(bool status) => FindByCondition(x => x.IsCompleted == status).Include(x => x.ShoppingItems);
        public IEnumerable<ShoppingList> GetShoppingListByOwnerId(string ownerId) => FindByCondition(x => x.OwnerId == ownerId).Include(x => x.ShoppingItems);

    }
}
