using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShowBridge.Models
{
    public class ProductModel
    {
        
        public long Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        [StringLength(100)]
        public string Description { get; set; }
        [Required]
        public bool Active { get; set; }
    }
}