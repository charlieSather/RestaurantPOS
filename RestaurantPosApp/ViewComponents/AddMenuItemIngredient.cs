using Microsoft.AspNetCore.Mvc;
using RestaurantPosApp.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantPosApp.ViewComponents
{
    public class AddMenuItemIngredient : ViewComponent
    {
        private readonly IRepositoryWrapper _repo;
        public AddMenuItemIngredient(IRepositoryWrapper repo)
        {
            _repo = repo;
        }
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
