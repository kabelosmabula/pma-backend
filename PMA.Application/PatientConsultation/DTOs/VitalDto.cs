using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.PatientConsultation.DTOs
{
    public class VitalDto
    {
        public int? oxygenSaturation { get; set; }
        public int? respiratoryRate { get; set; }
        public decimal? Temperature { get; set; }
        public int? Pulse { get; set; }
        public int? SystolicBP { get; set; }
        public int? DiastolicBP { get; set; }
        public decimal? Weight { get; set; }
        public decimal? height { get; set; }
    }
}
