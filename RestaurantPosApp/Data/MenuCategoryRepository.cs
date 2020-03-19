using RestaurantPosApp.Contracts;
using RestaurantPosApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantPosApp.Data
{
    public class MenuCategoryRepository : RepositoryBase<MenuCategory>, IMenuCategoryRepository
    {
        public MenuCategoryRepository(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        { 
        }

        public void CreateMenuCategory(MenuCategory menuCategory) => Create(menuCategory);

        public void deleteMenuCategory(MenuCategory menuCategory) => Delete(menuCategory);

        public IQueryable<MenuCategory> GetMenuCategories() => FindAll();

        public MenuCategory GetMenuCategory(int id) => FindByCondition(x => x.MenuCategoryId == id).FirstOrDefault();

        public MenuCategory GetMenuCategory(string name) => FindByCondition(x => x.CategoryName == name).FirstOrDefault();

        public bool MenuCategoryExists(string name) => FindAll().Any(x => x.CategoryName == name);

        public void updateMenuCategory(MenuCategory menuCategory) => Update(menuCategory);
    }
}
