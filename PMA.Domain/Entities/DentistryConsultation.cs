using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Domain.Entities
{
    public class DentistryConsultation : BaseEntity
    {
        public bool XRayCompleted { get; set; }
        public string GumCondition { get; set; }
        public ICollection<ToothChart> ToothCharts { get; set; } = new List<ToothChart>();
        public Guid ConsultationId { get; set; }
        public Consultation Consultation { get; set; }
    }
}
