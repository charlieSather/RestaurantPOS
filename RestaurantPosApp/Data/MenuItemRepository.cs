using Microsoft.EntityFrameworkCore;
using RestaurantPosApp.Contracts;
using RestaurantPosApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantPosApp.Data
{
    public class MenuItemRepository : RepositoryBase<MenuItem>, IMenuItemRepository
    {
        public MenuItemRepository(ApplicationDbContext applicationDbContext)
            :base(applicationDbContext)
        {
        }
        public void CreateMenuItem(MenuItem menuItem) => Create(menuItem);
        public void DeleteMenuItem(MenuItem menuItem) => Delete(menuItem);
        public void UpdateMenuItem(MenuItem menuItem) => Update(menuItem);

        public MenuItem GetMenuItem(int id) =>
            FindByCondition(x => x.MenuItemId == id)
                .Include(mc => mc.MenuCategory)
                .Include(menuItem => menuItem.Recipe).ThenInclude(i => i.Ingredient)
                .FirstOrDefault();

        public IQueryable<MenuItem> GetMenuItemsByCategory(int categoryId) =>
             FindByCondition(x => x.MenuCategoryId == categoryId)
                 .Include(mc => mc.MenuCategory)
                 .Include(menuItem => menuItem.Recipe).ThenInclude(i => i.Ingredient);

    }
}
