using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Domain.Entities
{
    public class HouseholdInvoiceItem : BaseEntity
    {
        public Guid HouseholdInvoiceId { get; set; }
        public HouseholdInvoice HouseholdInvoice { get; set; }
        public Guid InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
    }
}
