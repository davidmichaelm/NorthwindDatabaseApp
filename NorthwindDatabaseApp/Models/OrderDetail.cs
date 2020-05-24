using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NorthwindConsole.Models
{
    public class OrderDetail
    {
        [Key]
        public int OrderID { get; set; }
        
        [Required(ErrorMessage = "ProductID is required")]
        public int ProductID { get; set; }
        
        [Required(ErrorMessage = "UnitPrice is required")]
        public decimal UnitPrice { get; set; }
        
        [Required(ErrorMessage = "Quantity is required")]
        public short Quantity { get; set; }
        
        [Required(ErrorMessage = "Discount is required")]
        public decimal Discount { get; set; }
        
        public virtual List<Product> Products { get; set; }

    }
}