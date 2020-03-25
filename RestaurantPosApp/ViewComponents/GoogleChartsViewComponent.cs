using Microsoft.AspNetCore.Mvc;
using RestaurantPosApp.Contracts;
using RestaurantPosApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantPosApp.ViewComponents
{
    public class GoogleChartsViewComponent : ViewComponent
    {
        private readonly IRepositoryWrapper _repo;
        public GoogleChartsViewComponent(IRepositoryWrapper repo)
        {
            _repo = repo;
        }
        private Task<List<InventoryItem>> GetItemsAsync()
        {
            return Task.Run(() => _repo.InventoryItem.GetInventoryItems().ToList());
        }
        public async Task<IViewComponentResult> InvokeAsync(int categoryId)
        {
            var inventory = await GetItemsAsync();
            return View("Default", new StatisticsViewModel { Inventory = inventory });
        }
    }
}
