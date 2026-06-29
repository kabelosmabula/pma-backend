using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Domain.Entities
{
    public class PediatricsConsultation : BaseEntity
    {
        public decimal WeightKg { get; set; }
        public decimal HeightCm { get; set; }
        public bool VaccinationsUpToDate { get; set; }
        public string DevelopmentalMilestones { get; set; }
        public Guid ConsultationId { get; set; }
        public Consultation Consultation { get; set; }
    }
}
