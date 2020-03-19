using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantPosApp.ViewComponents
{
    public class CreateMenuCategory : ViewComponent
    {
        public CreateMenuCategory()
        {
        }

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
