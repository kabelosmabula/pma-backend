using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.PatientConsultation.DTOs
{
    public class clinicalDocumentsDto
    {
        public string documentName { get; set; }
        public string documentType { get; set; }
        public string fileUrl { get; set; }
    }
}
