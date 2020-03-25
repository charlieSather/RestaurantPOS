using Microsoft.AspNetCore.Mvc;
using RestaurantPosApp.Contracts;
using RestaurantPosApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantPosApp.ViewComponents
{
    public class GetMenuItemsByCategoryViewComponent : ViewComponent
    {
        private readonly IRepositoryWrapper _repo;
        public GetMenuItemsByCategoryViewComponent(IRepositoryWrapper repo)
        {
            _repo = repo;
        }

        private Task<List<MenuItem>> GetItemsAsync(int categoryId)
        {
            return Task.Run(() => _repo.MenuItem.GetMenuItemsByCategory(categoryId).ToList());
        }
        public async Task<IViewComponentResult> InvokeAsync(int categoryId)
        {
            var items = await GetItemsAsync(categoryId);
            return View("Default", items);
        }
    }
}
