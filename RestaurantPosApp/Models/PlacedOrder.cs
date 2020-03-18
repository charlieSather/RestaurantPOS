using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantPosApp.Models
{
    public class PlacedOrder
    {
        public int PlacedOrderId { get; set; }
        public DateTime OrderedTimestamp { get; set; }
    }
}
