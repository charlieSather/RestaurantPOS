using Microsoft.EntityFrameworkCore;
using RestaurantPosApp.Contracts;
using RestaurantPosApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantPosApp.Data
{
    public class OrderMenuItemRepository : RepositoryBase<OrderMenuItem>, IOrderMenuItemRepository
    {
        public OrderMenuItemRepository(ApplicationDbContext applicationDbContext)
            :base(applicationDbContext)
        {
        }

        public void CreateOrderMenuItem(OrderMenuItem orderMenuItem) => Create(orderMenuItem);

        public IQueryable<OrderMenuItem> GetOrderMenuItemsByOrderId(int orderId) =>
            FindByCondition(x => x.PlacedOrderId == orderId)
            .Include(m => m.MenuItem)
            .Include(o => o.PlacedOrder);
        public void UpdateOrderMenuItem(OrderMenuItem orderMenuItem) => Update(orderMenuItem);

        public int NumberSoldByDateRange(int menuItemId, DateTime start, DateTime end) =>
            FindByCondition(x => x.MenuItemId == menuItemId && x.PlacedOrder.OrderedTimestamp >= start && x.PlacedOrder.OrderedTimestamp <= end)
                .Sum(x => x.Quantity);

        public void UpdateOrderMenuItems(List<OrderMenuItem> orderMenuItems)
        {
            foreach (var orderMenuItem in orderMenuItems)
            {
                Update(orderMenuItem);
            }
        }
        public IEnumerable<Tuple<MenuItem, int>> GetTopSellingMenuItemsByDate(DateTime start, DateTime end, int amount = 0)
        {
            var result = FindByCondition(x => x.PlacedOrder.OrderedTimestamp.Date >= start.Date && x.PlacedOrder.OrderedTimestamp.Date <= end.Date)
                            .Include(m => m.MenuItem)
                            .ToList()
                            .GroupBy(x => x.MenuItemId)
                            .Select(x => Tuple.Create(x.FirstOrDefault(m => x.Key == m.MenuItemId).MenuItem, x.Sum(q => q.Quantity)))
                            .OrderByDescending(x => x.Item2)
                            .Take(amount);
            return result;
        }

        public IQueryable<OrderMenuItem> GetOrderMenuItemsByMenuItemId(int menuItemId) => FindByCondition(x => x.MenuItemId == menuItemId);
    }
}
