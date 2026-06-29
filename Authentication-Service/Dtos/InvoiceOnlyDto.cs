namespace API.Service.Dtos
{
    public class InvoiceOnlyDto
    {
        public Guid id { get; set; }

        public Guid PatientId { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal OutStandingAmount { get; set; }

        public string Status { get; set; } = string.Empty;
    }
}
