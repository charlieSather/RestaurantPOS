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
        IQueryable<OrderMenuItem> GetOrderMenuItemsByOrderId(int orderId);
    }
}
