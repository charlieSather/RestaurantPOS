using RestaurantPosApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantPosApp.Contracts
{
    public interface IRestaurantRepository : IRepositoryBase<Restaurant>
    {
        void CreateRestaurant(Restaurant restaurant);
        void UpdateRestaurant(Restaurant restaurant);
        void DeleteRestaurant(Restaurant restaurant);
        Restaurant GetRestaurantByOwenerId(string ownerId);
    }
}
