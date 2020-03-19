using Microsoft.EntityFrameworkCore;
using RestaurantPosApp.Contracts;
using RestaurantPosApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantPosApp.Data
{
    public class OrderMenuRepository : RepositoryBase<OrderMenuItem>, IOrderMenuItemRepository
    {
        public OrderMenuRepository(ApplicationDbContext applicationDbContext)
            :base(applicationDbContext)
        {
        }

        public void CreateOrderMenuItem(OrderMenuItem orderMenuItem) => Create(orderMenuItem);

        public IQueryable<OrderMenuItem> GetOrderMenuItemsByOrderId(int orderId) =>
            FindByCondition(x => x.PlacedOrderId == orderId)
            .Include(m => m.MenuItem)
            .Include(o => o.PlacedOrder);
        public void UpdateOrderMenuItem(OrderMenuItem orderMenuItem) => Update(orderMenuItem);

        public void UpdateOrderMenuItems(List<OrderMenuItem> orderMenuItems)
        {
            foreach (var orderMenuItem in orderMenuItems)
            {
                Update(orderMenuItem);
            }
        }
    }
}
