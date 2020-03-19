using Microsoft.EntityFrameworkCore;
using RestaurantPosApp.Contracts;
using RestaurantPosApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RestaurantPosApp.Data
{
    public class PlacedOrderRepository : RepositoryBase<PlacedOrder>, IPlacedOrderRepository
    {
        public PlacedOrderRepository(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        {
        }

        public void CreateOrder(PlacedOrder placedOrder) => Create(placedOrder);
        public void DeleteOrder(PlacedOrder placedOrder) => Delete(placedOrder);
        public void UpdateOrder(PlacedOrder placedOrder) => Update(placedOrder);

        public PlacedOrder GetOrder(int id) =>
            FindByCondition(x => x.PlacedOrderId == id)
                .Include(items => items.OrderedItems).ThenInclude(x => x.MenuItem)
                .FirstOrDefault();

        public IQueryable<PlacedOrder> FilterOrders(Expression<Func<PlacedOrder, bool>> expression) =>
            FindByCondition(expression)
                .Include(u => u.IdentityUser)
                .Include(items => items.OrderedItems).ThenInclude(x => x.MenuItem);

        public IQueryable<PlacedOrder> GetOrdersByUserId(string userId) =>
            FindByCondition(x => x.UserId == userId)
                .Include(u => u.IdentityUser)
                .Include(items => items.OrderedItems).ThenInclude(x => x.MenuItem);

    }
}
