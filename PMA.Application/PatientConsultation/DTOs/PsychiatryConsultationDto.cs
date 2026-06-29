using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.PatientConsultation.DTOs
{
    public class PsychiatryConsultationDto
    {
        public string Mood { get; set; }
        public string Affect { get; set; }
        public string Speech { get; set; }
        public bool SuicidalIdeation { get; set; }
        public string SelfHarmRiskLevel { get; set; }
        public string TherapyNotes { get; set; }
    }
}
