using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantPosApp.Models
{
    public class OrderMenuItem
    {
        public int OrderMenuItemId { get; set; }

        [Range(1,15)]
        public int Quantity { get; set; }

        [ForeignKey("MenuItem")]
        public int MenuItemId { get; set; }
        public MenuItem MenuItem { get; set; }

        [ForeignKey("PlacedOrder")]
        public int PlacedOrderId { get; set; }
        public PlacedOrder PlacedOrder { get; set; }
    }
}
