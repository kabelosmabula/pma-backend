using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.PatientConsultation.DTOs
{
    public class OncologyConsultationDto
    {
        public string CancerType { get; set; }
        public string CancerStage { get; set; }
        public int ChemotherapyCycle { get; set; }
        public string TumorResponse { get; set; }
    }
}
