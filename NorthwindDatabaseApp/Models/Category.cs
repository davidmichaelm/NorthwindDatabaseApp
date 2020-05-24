using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NorthwindConsole.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        
        [Required(ErrorMessage = "Category Name is required")]
        [StringLength(15, ErrorMessage = "Category Name must be 15 characters or less")]
        public string CategoryName { get; set; }
        public string Description { get; set; }

        public virtual List<Product> Products { get; set; }
    }
}