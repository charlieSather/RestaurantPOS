using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantPosApp.Models
{
    public class MenuItem
    {
        public int MenuItemId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        
        [ForeignKey("MenuCategory")]
        public int MenuCategoryId { get; set; }
        public MenuCategory MenuCategory { get; set; }
    }
}
