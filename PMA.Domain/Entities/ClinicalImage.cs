using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Domain.Entities
{
    public class ClinicalImage : BaseEntity
    {
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public DateTime CapturedAt { get; set; }
        public Guid DermatologyConsultationId { get; set; }
        public DermatologyConsultation DermatologyConsultation { get; set; }
    }
}
