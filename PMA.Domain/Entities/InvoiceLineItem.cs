using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Domain.Entities
{
    public class InvoiceLineItem : BaseEntity
    {
        public Guid InvoiceId { get; set; }
        public string ReferenceCode { get; set; } 
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public Invoice Invoice { get; set; } = null!;
    }
}
