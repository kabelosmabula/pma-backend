using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Domain.Entities
{
    public class EmergencyContact : BaseEntity
    {
        public string name { get; set; }
        public string relationship { get; set; }
        public string phone { get; set; }
        public Guid PatientId { get; set; }
        public Patient Patient { get; set; } = null!;
    }
}
