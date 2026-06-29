using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.PatientConsultation.DTOs
{
    public class proceduresDto
    {
        public string name { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public decimal tariffAmount { get; set; }
    }
}
