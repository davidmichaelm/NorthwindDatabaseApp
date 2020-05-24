using System;
using System.ComponentModel.DataAnnotations;

namespace NorthwindConsole.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        
        [Required(ErrorMessage = "Product Name is required")]
        [StringLength(40, ErrorMessage = "Product Name must be 40 characters or less")]
        public string ProductName { get; set; }
        
        [StringLength(20, ErrorMessage = "Quantity Per Unit must be 20 characters or less")]
        public string QuantityPerUnit { get; set; }
        
        public decimal? UnitPrice { get; set; }
        public Int16? UnitsInStock { get; set; }
        public Int16? UnitsOnOrder { get; set; }
        public Int16? ReorderLevel { get; set; }
        
        [Required(ErrorMessage = "Discontinued is required")]
        public bool Discontinued { get; set; }

        public int? CategoryId { get; set; }
        public int? SupplierId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}