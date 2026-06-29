using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Domain.Entities
{
    public class HouseHoldInvoiceCollection:BaseEntity
    {
        public int invoiceId { get; set; }
        public Invoice Invoice { get; set; }
        public int patientid { get; set; }
        public Patient Patient { get; set; }
        public int practiceid { get; set; }
        public Practice Practice { get; set; }
    }
}
