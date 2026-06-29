using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Domain.Entities
{
    public class DermatologyConsultation : BaseEntity
    {
        public string SkinCondition { get; set; }
        public string Severity { get; set; }
        public string AffectedAreas { get; set; }
        public ICollection<ClinicalImage> ClinicalImages { get; set; } = new List<ClinicalImage>();
        public Guid ConsultationId { get; set; }
        public Consultation Consultation { get; set; }
    }
}
