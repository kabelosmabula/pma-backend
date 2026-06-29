using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Domain.Entities
{
    public class Payment: BaseEntity
    {
        public string PaymentMethod { get; set; } = string.Empty;

        public decimal Amount { get; set; }

        public string Status { get; set; } = string.Empty;

        public  Guid InvoiceId { get; set; }

        public Invoice Invoice { get; set; } = null!;

    }
}
