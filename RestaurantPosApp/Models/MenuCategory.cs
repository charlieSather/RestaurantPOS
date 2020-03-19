using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantPosApp.Models
{
    public class MenuCategory
    {
        public int MenuCategoryId { get; set; }

        [Required(ErrorMessage ="Menu category name is required")]
        [Display(Name ="Menu Category Name")]
        public string CategoryName { get; set; }
    }
}