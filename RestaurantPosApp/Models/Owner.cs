using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantPosApp.Models
{
    public class Owner
    {
        [Key]
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public int PinCode { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }


        [ForeignKey("IdentityUser")]
        public string userId { get; set; }
        public IdentityUser IdentityUser { get; set; }
    }
}
