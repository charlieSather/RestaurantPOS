using RestaurantPosApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantPosApp.Contracts
{
    public interface IInventoryItemRepository : IRepositoryBase<InventoryItem>
    {
        void CreateInventoryItem(InventoryItem inventoryItem);
        void UpdateInventoryItem(InventoryItem inventoryItem);
        void UpdateInventoryItems(List<InventoryItem> inventoryItems);
        void DeleteInventoryItem(InventoryItem inventoryItem);
        InventoryItem GetInventoryItem(int id);
        InventoryItem GetInventoryItemByIngredientId(int ingredientId);
        IQueryable<InventoryItem> GetInventoryItems();
        IQueryable<InventoryItem> GetInventoryItemsByIngredientId(int ingredientId);
        IQueryable<InventoryItem> GetInventoryItemsByIngredientList(List<int> ingredientIds);

    }
}
