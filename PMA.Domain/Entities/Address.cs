using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Domain.Entities
{
    public class Address : BaseEntity
    {
        public string streetAddress { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string postalCode { get; set; }
        public Guid PatientId { get; set; }
        public Patient Patient { get; set; } = null!;
    }
}
