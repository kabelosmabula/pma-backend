using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Domain.Entities
{
    public class Invoice : BaseEntity
    {
        public Guid? PatientMedicalAidId { get; set; }

        public Guid ConsultationId { get; set; }

        public Guid PatientId { get; set; } 

        public decimal TotalAmount { get; set; }

        public decimal OutStandingAmount { get; set; }

        public Guid HouseholdId { get; set; }

        public string Status { get; set; } = string.Empty;

        public Consultation Consultation { get; set; } = null!;

        public ICollection<InvoiceLineItem> LineItems { get; set; } = new List<InvoiceLineItem>();

        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
