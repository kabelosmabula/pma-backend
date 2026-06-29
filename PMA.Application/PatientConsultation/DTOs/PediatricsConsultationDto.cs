using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.PatientConsultation.DTOs
{
    public class PediatricsConsultationDto
    {
        public decimal WeightKg { get; set; }
        public decimal HeightCm { get; set; }
        public bool VaccinationsUpToDate { get; set; }
        public string DevelopmentalMilestones { get; set; }
    }
}
