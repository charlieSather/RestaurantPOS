using RestaurantPosApp.Contracts;
using RestaurantPosApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantPosApp.Data
{
    public class RestaurantRepository : RepositoryBase<Restaurant>, IRestaurantRepository
    {
        public RestaurantRepository(ApplicationDbContext applicationDbContext)
            :base(applicationDbContext)
        {
        }
        public void CreateRestaurant(Restaurant restaurant) => Create(restaurant);
        public void DeleteRestaurant(Restaurant restaurant) => Delete(restaurant);
        public void UpdateRestaurant(Restaurant restaurant) => Update(restaurant);
        public Restaurant GetRestaurantByOwenerId(int ownerId) => FindByCondition(x => x.OwnerId == ownerId).FirstOrDefault();
    }
}
