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
        public void UpdateRangeOfInventoryItems(IEnumerable<InventoryItem> inventoryItems) => UpdateRange(inventoryItems);
        public int GetSumForIngredient(int ingredientId) => FindByCondition(x => x.IngredientId == ingredientId).Sum(x => x.AmountInGrams);

        public InventoryItem GetInventoryItem(int id) => FindByCondition(x => x.InventoryItemId == id).FirstOrDefault();

        public InventoryItem GetInventoryItemByIngredientId(int ingredientId) => FindByCondition(x => x.IngredientId == ingredientId).FirstOrDefault();

        public IQueryable<InventoryItem> GetInventoryItems() => FindAll().Include(i => i.Ingredient);

        public IQueryable<InventoryItem> GetInventoryItemsByIngredientId(int ingredientId) => FindByCondition(x => x.IngredientId == ingredientId);

        public IEnumerable<InventoryItem> GetInventoryItemsByIngredientList(List<int> ingredientIds) =>
                ingredientIds.Join(FindAll().Include(x => x.Ingredient), ingredientId => ingredientId, inventoryItem => inventoryItem.IngredientId, (ingredientId, inventoryItem) => inventoryItem);
        public IEnumerable<InventoryItem> GetLowInventoryItems() => FindByCondition(x => x.IsLow == true).Include(i => i.Ingredient);
    }
}
