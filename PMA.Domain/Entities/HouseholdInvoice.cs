using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Domain.Entities
{
    public class HouseholdInvoice : BaseEntity
    {
        public Guid HouseholdId { get; set; }
        public PatientHousehold Household { get; set; }
        public DateTime BillingPeriodStart { get; set; }
        public DateTime BillingPeriodEnd { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public ICollection<HouseholdInvoiceItem> Items { get; set; } = new List<HouseholdInvoiceItem>();
    }
}
