using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantPosApp.Models
{
    public class StatisticsViewModel
    {
        public List<PlacedOrder> Orders { get; set; }
        public List<MenuItem> MenuItems { get; set; }
        public List<InventoryItem> Inventory { get; set; }
    }
}
