using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.PatientConsultation.DTOs
{
    public class prescriptionsDto
    {
        public string medicationName { get; set; }
        public string dosage { get; set; }
        public string frequency { get; set; }
        public string notes { get; set; }
        public string duration { get; set; }
        public DateTime StartDate { get; set; }
    }
}
