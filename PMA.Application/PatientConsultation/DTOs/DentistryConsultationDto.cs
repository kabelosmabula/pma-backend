using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.PatientConsultation.DTOs
{

    public class DentistryConsultationDto
    {
        public bool XRayCompleted { get; set; }
        public string GumCondition { get; set; }
        public List<ToothChartDto> ToothCharts { get; set; }
    }
}
