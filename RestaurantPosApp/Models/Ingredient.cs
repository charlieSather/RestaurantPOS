using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantPosApp.Models
{
    public class Ingredient
    {
        public int IngredientId { get; set; }

        [Required]
        [Display(Name = "Ingredient Name")]
        public string Name { get; set; }

        [Required]
        public int BaseUnitOfWeight { get; set; }

        [Required]
        [Column(TypeName = ("decimal(12,2)"))]
        public decimal PricePerUnit { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is Ingredient ingredient))
            {
                return false;
            }
            return ingredient.IngredientId == this.IngredientId;
        }
        public override int GetHashCode()
        {
            return IngredientId;
        }
    }
}
