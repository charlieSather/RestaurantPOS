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
        void AddRangeOfInventoryItems(IEnumerable<InventoryItem> inventoryItems);
        void UpdateInventoryItem(InventoryItem inventoryItem);
        void UpdateRangeOfInventoryItems(IEnumerable<InventoryItem> inventoryItems);
        void DeleteInventoryItem(InventoryItem inventoryItem);
        int GetSumForIngredient(int ingredientId);
        InventoryItem GetInventoryItem(int id);
        InventoryItem GetInventoryItemByIngredientId(int ingredientId);
        IQueryable<InventoryItem> GetInventoryItems();
        IQueryable<InventoryItem> GetInventoryItemsByIngredientId(int ingredientId);
        IEnumerable<InventoryItem> GetInventoryItemsByIngredientList(List<int> ingredientIds);
        IEnumerable<InventoryItem> GetLowInventoryItems();
    }
}
