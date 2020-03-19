using RestaurantPosApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RestaurantPosApp.Contracts
{
    public interface IPlacedOrderRepository : IRepositoryBase<PlacedOrder>
    {
        void CreateOrder(PlacedOrder placedOrder);
        void UpdateOrder(PlacedOrder placedOrder);
        void DeleteOrder(PlacedOrder placedOrder);
        PlacedOrder GetOrder(int id);
        IQueryable<PlacedOrder> GetOrdersByUserId(string userId);
        IQueryable<PlacedOrder> FilterOrders(Expression<Func<PlacedOrder, bool>> expression);

    }
}
