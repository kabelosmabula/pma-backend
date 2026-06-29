using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Domain.Entities
{
    public class Organisation : BaseEntity
    {
        public string Name { get; set; }
        public string MedicalAidName { get; set; }
        public string MedicalAidNumber { get; set; }
        public ICollection<Patient> Patients { get; set; }
        public Guid? MedicalAidId { get; set; }
    }
}
