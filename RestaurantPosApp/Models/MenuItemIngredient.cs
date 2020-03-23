using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantPosApp.Models
{
    public class MenuItemIngredient
    {
        public int MenuItemIngredientId { get; set; }

        [Display(Name="Amount in Grams")]
        public int Quantity { get; set; }


        [Column(TypeName = ("decimal(12,2)"))]
        public decimal Cost { get; set; }

        
        [ForeignKey("Ingredient")]
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }


        [ForeignKey("MenuItem")]
        public int MenuItemId { get; set; }
        public MenuItem MenuItem { get; set; }
    }
}
