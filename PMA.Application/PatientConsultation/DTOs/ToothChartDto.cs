using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.PatientConsultation.DTOs
{
    public class ToothChartDto
    {
        public int ToothNumber { get; set; }
        public string Condition { get; set; }
        public string Notes { get; set; }
    }
}
