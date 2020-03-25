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


        [ForeignKey("Owner")]
        public string OwnerId { get; set; }
        public IdentityUser Owner { get; set; }
    }
}
