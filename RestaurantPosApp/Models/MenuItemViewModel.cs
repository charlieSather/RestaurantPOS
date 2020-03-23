using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantPosApp.Models
{
    public class MenuItemViewModel
    {
        public MenuItem MenuItem { get; set; }
        public MenuItemIngredient MenuItemIngredient { get; set; }
        public List<MenuItemIngredient> Recipe { get; set; }
        public SelectList MenuCategories { get; set; }
        public SelectList Ingredients { get; set; }

    }
}
