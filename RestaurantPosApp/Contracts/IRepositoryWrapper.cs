using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantPosApp.Contracts
{
    public interface IRepositoryWrapper
    {
        IIngredientRepository Ingredient { get; }
        IInventoryItemRepository InventoryItem { get; }
        IMenuCategoryRepository MenuCategory { get; }
        IMenuItemIngredientRepository MenuItemIngredient { get; }
        IMenuItemRepository MenuItem { get; }
        IOrderMenuItemRepository OrderMenuItem { get; }
        IPlacedOrderRepository PlacedOrder { get; }
        IRestaurantRepository Restaurant { get; }
        void Save();
    }
}
