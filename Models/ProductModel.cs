using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShowBridge.Models
{
    public class ProductModel
    {
        
        public long ID { get; set; }
        [Required]
        [StringLength(50)]
        public string NAME { get; set; }
        [Required]
        public decimal PRICE { get; set; }
        [Required]
        [StringLength(100)]
        public string DESCRIPTION { get; set; }
        [Required]
        public bool ACTIVE { get; set; }
    }
}