using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantPosApp.Models
{
    public class MenuItem
    {
        [Key]
        public int MenuItemId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Column(TypeName = ("decimal(12,2)"))]
        public decimal Price { get; set; }

        public bool Disabled { get; set; }

        public List<MenuItemIngredient> Recipe { get; set; }


        [ForeignKey("MenuCategory")]
        public int MenuCategoryId { get; set; }
        public MenuCategory MenuCategory { get; set; }
    }
}
