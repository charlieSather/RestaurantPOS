using RestaurantPosApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantPosApp.Contracts
{
    public interface IShoppingListRepository
    {
        void CreateShoppingList(ShoppingList shoppingList);
        void UpdateShoppingList(ShoppingList shoppingList);
        void DeleteShoppingList(ShoppingList shoppingList);
        IEnumerable<ShoppingList> GetShoppingListByOwnerId(string ownerId);
        IEnumerable<ShoppingList> GetShoppingListByStatus(bool status);

    }
}
