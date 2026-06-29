using PMA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.PatientConsultation.DTOs
{
    public class DiagnosisDto
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
