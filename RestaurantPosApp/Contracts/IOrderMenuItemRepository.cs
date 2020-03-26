using RestaurantPosApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantPosApp.Contracts
{
    public interface IOrderMenuItemRepository : IRepositoryBase<OrderMenuItem>
    {
        void CreateOrderMenuItem(OrderMenuItem orderMenuItem);
        void UpdateOrderMenuItem(OrderMenuItem orderMenuItem);
        void UpdateOrderMenuItems(List<OrderMenuItem> orderMenuItems);
        int NumberSoldByDateRange(int menuItemId, DateTime start, DateTime end);
        IQueryable<OrderMenuItem> GetOrderMenuItemsByOrderId(int orderId);
        IQueryable<OrderMenuItem> GetOrderMenuItemsByMenuItemId(int menuItemId);
        IEnumerable<Tuple<MenuItem, int>> GetTopSellingMenuItemsByDate(DateTime start, DateTime end, int amount = 0);    
    }
}
