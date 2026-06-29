using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Domain.Entities
{
    public class OncologyConsultation : BaseEntity
    {
        public string CancerType { get; set; }
        public string CancerStage { get; set; }
        public int ChemotherapyCycle { get; set; }
        public string TumorResponse { get; set; }
        public Guid ConsultationId { get; set; }
        public Consultation Consultation { get; set; }
    }
}
