using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantPosApp.Models
{
    public class InventoryItem
    {
        public int InventoryItemId { get; set; }

        [Range(0, 1000000)]
        public int AmountInGrams { get; set; }

        [Column(TypeName = ("decimal(12,2)"))]
        public decimal BulkPrice { get; set; }

        [Range(0, 10000)]
        public int LowerThreshold { get; set; }
        public bool IsLow { get; set; }


        [ForeignKey("Ingredient")]
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }
    }
}
