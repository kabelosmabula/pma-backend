using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Domain.Entities
{
    public class Allergy : BaseEntity
    {
        public Guid PatientId { get; set; }
        public string AllergyName { get; set; }
        public Patient Patient { get; set; }
    }
}
