using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Domain.Entities
{
    public class Diagnosis : BaseEntity
    {
        public Guid ConsultationId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public Consultation DoctorConsultation { get; set; }
    }
}
