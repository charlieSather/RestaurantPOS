using RestaurantPosApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantPosApp.Contracts
{
    public interface IMenuItemRepository : IRepositoryBase<MenuItem>
    {
        void CreateMenuItem(MenuItem menuItem);
        void UpdateMenuItem(MenuItem menuItem);
        void DeleteMenuItem(MenuItem menuItem);
        MenuItem GetMenuItem(int id);
        IQueryable<MenuItem> GetMenuItemsByCategory(int categoryId);
        IEnumerable<MenuItem> GetMenuItemsByIds(IEnumerable<int> menuItemIds);
    }
}
