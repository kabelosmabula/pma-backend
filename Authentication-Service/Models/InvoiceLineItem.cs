using System.ComponentModel.DataAnnotations;

namespace API.Service.Models
{
    public class InvoiceLineItem
    {
        [Required(ErrorMessage ="Reference code is required.")]
        public string ReferenceCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Amount is required.")]
        public decimal Amount { get; set; } 
    }
}
