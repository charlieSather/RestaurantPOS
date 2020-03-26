using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantPosApp.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        
        [Required]
        public string Name { get; set; }

        [RegularExpression(@"^[0-9]{4}$", ErrorMessage ="Pin Code must be a 4-digit Code")]
        [Required]
        [Display(Name ="Pin Code")]
        public int PinCode { get; set; }

        [ForeignKey("IdentityUser")]
        public string UserId { get; set; }
        public IdentityUser IdentityUser { get; set; }
    }
}
