using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Domain.Entities
{
    public class Vital : BaseEntity
    {
        public Guid consultationId { get;  set; }
        public Consultation consultation { get;  set; }
        public int? oxygenSaturation { get; set; }
        public int? respiratoryRate { get; set; }
        public decimal? Temperature { get;  set; }
        public int? Pulse { get;  set; }
        public int? SystolicBP { get;  set; }
        public int? DiastolicBP { get;  set; }
        public decimal? Weight { get;  set; }
        public decimal? height { get; set; }
        public decimal? BMI { get; set; }
    }
}
