using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantPosApp.Models
{
    public class IngredientShoppingList
    {
        [Key]
        public int IngredientShoppingListId { get; set; }

        [Display(Name ="Amount in Grams")]
        public int AmountInGrams { get; set; }

        [ForeignKey("Ingredient")]
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }

        [ForeignKey("ShoppingList")]
        public int ShoppingListId { get; set; }
        public ShoppingList ShoppingList { get; set; }
    }
}
