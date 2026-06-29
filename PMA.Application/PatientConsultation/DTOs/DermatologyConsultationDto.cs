using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.PatientConsultation.DTOs
{
    public class DermatologyConsultationDto
    {
        public string SkinCondition { get; set; }
        public string Severity { get; set; }
        public string AffectedAreas { get; set; }
        public List<ClinicalImageDto> ClinicalImages { get; set; }
    }
}
