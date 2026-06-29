using System.ComponentModel.DataAnnotations;

namespace API.Service.SharedModels
{
    public class PayInvoiceShared
    {
        [Required(ErrorMessage = "InvoiceId is required.")]
        public Guid InvoiceId { get; set; }

        [AllowedValues(values: ["Cash", "Medical Aid", "EFT"])]
        [Required(ErrorMessage = "Payment Method is required.")]
        public string PaymentMethod { get; set; } = string.Empty;

        [Required(ErrorMessage = "Amount is required.")]
        public decimal Amount { get; set; }
    }
}
