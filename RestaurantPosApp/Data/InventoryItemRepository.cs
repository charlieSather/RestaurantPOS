using Microsoft.EntityFrameworkCore;
using RestaurantPosApp.Contracts;
using RestaurantPosApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantPosApp.Data
{
    public class InventoryItemRepository : RepositoryBase<InventoryItem>, IInventoryItemRepository
    {
        public InventoryItemRepository(ApplicationDbContext applicationDbContext)
            :base(applicationDbContext)
        {
        }

        public void CreateInventoryItem(InventoryItem inventoryItem) => Create(inventoryItem);
        public void DeleteInventoryItem(InventoryItem inventoryItem) => Delete(inventoryItem);
        public void UpdateInventoryItem(InventoryItem inventoryItem) => Update(inventoryItem);
        public void AddRangeOfInventoryItems(IEnumerable<InventoryItem> inventoryItems) => AddRange(inventoryItems);

        public void UpdateInventoryItems(List<InventoryItem> inventoryItems)
        {
            foreach (var inventoryItem in inventoryItems)
            {
                Update(inventoryItem);
            }
        }

        public InventoryItem GetInventoryItem(int id) => FindByCondition(x => x.InventoryItemId == id).FirstOrDefault();

        public InventoryItem GetInventoryItemByIngredientId(int ingredientId) => FindByCondition(x => x.IngredientId == ingredientId).FirstOrDefault();

        public IQueryable<InventoryItem> GetInventoryItems() => FindAll();

        public IQueryable<InventoryItem> GetInventoryItemsByIngredientId(int ingredientId) => FindByCondition(x => x.IngredientId == ingredientId);

        public IQueryable<InventoryItem> GetInventoryItemsByIngredientList(List<int> ingredientIds) => 
            FindAll()
                .Join(ingredientIds, inventoryItem => inventoryItem.IngredientId, ingredientId => ingredientId, (inventoryItem, ingredientId) => inventoryItem)
                .Include(i => i.Ingredient);

        //public IQueryable<InventoryItem> GetInventoryItemsByIngredientList(List<int> ingredientIds) => FindByCondition(x => ingredientIds.Contains(x.IngredientId));

    }
}
