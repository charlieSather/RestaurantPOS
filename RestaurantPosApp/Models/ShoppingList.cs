using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantPosApp.Models
{
    public class ShoppingList
    {
        [Key]
        public int ShoppingListId { get; set; }
        public bool IsCompleted { get; set; }
        public List<ShoppingListIngredient> ShoppingItems { get; set; }

        /// <summary>
        ///  Add DateTime properties to track shopping list's lifespan
        /// </summary>


        [ForeignKey("Owner")]
        public string OwnerId { get; set; }
        public IdentityUser Owner { get; set; }
    }
}
