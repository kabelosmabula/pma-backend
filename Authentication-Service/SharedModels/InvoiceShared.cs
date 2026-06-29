using API.Service.Models;
using System.ComponentModel.DataAnnotations;

namespace API.Service.SharedModels
{
    public class InvoiceShared
    {
        [Required(ErrorMessage = "ConsultationId  is required.")]
        public Guid ConsultationId { get; set; }

        [Required(ErrorMessage = "PatientId  is required.")]
        public Guid PatientId { get; set; }

        [Required(ErrorMessage = "TotalAmount  is required.")]
        public decimal TotalAmount { get; set; }

        [Required(ErrorMessage = "LineItems are required.")]
        public List<InvoiceLineItem> LineItems { get; set; } = new List<InvoiceLineItem>();
    }
}
