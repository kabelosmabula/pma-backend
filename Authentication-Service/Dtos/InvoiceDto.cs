using PMA.Domain.Entities;

namespace API.Service.Dtos
{
    public class InvoiceDto
    {
        public Guid id { get; set; }

        public Guid PatientId { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal OutStandingAmount { get; set; }

        public string Status { get; set; } = string.Empty;

        public List<LineItemsDto?> LineItems { get; set; } = new List<LineItemsDto?>();

        public ICollection<PaymentsDto> Payments { get; set; } = new List<PaymentsDto>();

    }

    public class LineItemsDto
    {
        public Guid id { get; set; }

        public string ReferenceCode { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Amount { get; set; }

        public DateTime createddate { get; set; }

        public string createdby { get; set; } = string.Empty;
    }

    public class PaymentsDto
    {
        public Guid id { get; set; }

        public string PaymentMethod { get; set; } = string.Empty;

        public decimal Amount { get; set; }

        public string Status { get; set; } = string.Empty;

        public Guid InvoiceId { get; set; }

        public DateTime createddate { get; set; }

        public string createdby { get; set; } = string.Empty;
    }




}
