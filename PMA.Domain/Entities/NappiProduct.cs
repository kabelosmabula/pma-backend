using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PMA.Domain.Entities
{
    public class NappiProduct : BaseEntity
    {
        [Key]
        [MaxLength(20)]
        public string NappiCode { get; set; }
        [Required]
        public string ProductName { get; set; }
        public string GenericName { get; set; }
        public string DosageForm { get; set; }   
        public string Strength { get; set; } 
        public string PackSize { get; set; }   

        public int? ManufacturerId { get; set; }
        public ICollection<NappiPricing> Prices { get; set; }

    }
}
