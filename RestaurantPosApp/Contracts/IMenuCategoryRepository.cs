using RestaurantPosApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantPosApp.Contracts
{
    public interface IMenuCategoryRepository : IRepositoryBase<MenuCategory>
    {
        void CreateMenuCategory(MenuCategory menuCategory);
        void updateMenuCategory(MenuCategory menuCategory);
        void deleteMenuCategory(MenuCategory menuCategory);
        IQueryable<MenuCategory> GetMenuCategories();
        MenuCategory GetMenuCategory(int id);
        MenuCategory GetMenuCategory(string name);
        bool MenuCategoryExists(string name);

    }
}
