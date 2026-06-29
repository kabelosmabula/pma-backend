using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PMA.Domain.Entities
{
    public class NappiPricing : BaseEntity
    {
        [Required]
        public string NappiCode { get; set; }
        public NappiProduct Product { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
        public DateTime EffectiveDate { get; set; } 
    }
}
