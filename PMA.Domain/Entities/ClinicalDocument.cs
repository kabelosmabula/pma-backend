using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Domain.Entities
{
    public class ClinicalDocument : BaseEntity
    {
        public Guid ConsultationId { get; set; }
        public string documentName { get; set; }
        public string documentType { get; set; }
        public string fileUrl { get; set; }
        public Consultation DoctorConsultation { get; set; }
    }
}
