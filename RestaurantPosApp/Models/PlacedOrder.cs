using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantPosApp.Models
{
    public class PlacedOrder
    {
        public int PlacedOrderId { get; set; }
        public DateTime OrderedTimestamp { get; set; }

        [Column(TypeName = ("decimal(12,2)"))]
        public decimal Total { get; set; }

        public List<OrderMenuItem> OrderedItems { get; set; }

        [ForeignKey("IdentityUser")]
        public string UserId { get; set; }
        public IdentityUser IdentityUser { get; set; }


    }
}
