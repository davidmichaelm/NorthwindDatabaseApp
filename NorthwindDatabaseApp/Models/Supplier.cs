﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NorthwindConsole.Models
{
    public class Supplier
    {
        public int SupplierId { get; set; }
        
        [Required(ErrorMessage = "Company Name is required")]
        [StringLength(40)]
        public string CompanyName { get; set; }
        
        [StringLength(30, ErrorMessage = "Contact Name must be 30 characters or less")]
        public string ContactName { get; set; }
        
        [StringLength(30, ErrorMessage = "Contact Title must be 30 characters or less")]
        public string ContactTitle { get; set; }
        
        [StringLength(60, ErrorMessage = "Address must be 60 characters or less")]
        public string Address { get; set; }
        
        [StringLength(15, ErrorMessage = "City must be 15 characters or less")]
        public string City { get; set; }
        
        [StringLength(15, ErrorMessage = "Region must be 15 characters or less")]
        public string Region { get; set; }
        
        [StringLength(10, ErrorMessage = "Postal Code must be 10 characters or less")]
        public string PostalCode { get; set; }
        
        [StringLength(15, ErrorMessage = "Country must be 15 characters or less")]
        public string Country { get; set; }
        
        [StringLength(24, ErrorMessage = "Phone must be 24 characters or less")]
        public string Phone { get; set; }
        
        [StringLength(24, ErrorMessage = "Fax must be 24 characters or less")]
        public string Fax { get; set; }

        public virtual List<Product> Products { get; set; }
    }
}
