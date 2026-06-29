using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Domain.Entities
{
    public class Procedure : BaseEntity
    {
        public Guid ConsultationId { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public decimal tariffAmount { get; set; }
        public Consultation DoctorConsultation { get; set; }
    }
}
