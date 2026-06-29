using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PMA.Domain.Entities
{
    public class ClinicalRecord : BaseEntity
    {
        public Guid PracticeId { get; set; }
        public Guid PatientId { get; set; }
        public Practice Practice { get; set; }
        public Patient Patient { get; set; }
        public ICollection<Consultation> DoctorVisits { get; set; } = new List<Consultation>();
    }

}
