using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.PatientConsultation.DTOs
{
    public class CardiologyConsultationDto
    {
        public bool ECGPerformed { get; set; }
        public string ECGFindings { get; set; }
        public bool EchoPerformed { get; set; }
        public int? EjectionFraction { get; set; }
        public bool SmokingRisk { get; set; }
        public bool HypertensionRisk { get; set; }
    }
}
