using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Domain.Entities
{
    public class PatientReferral : BaseEntity
    {
        public string name { get; set; }
        public string specialty { get; set; }
        public string practice { get; set; }
        public int phone { get; set; }
        public string email { get; set; }
        public string reason { get; set; }
    }
}
