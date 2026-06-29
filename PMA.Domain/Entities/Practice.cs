using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PMA.Domain.Entities
{

    public class Practice:BaseEntity
    {

        public string Name { get;  set; } = string.Empty;   
        public string PracticeNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public ICollection<Patient> Patients { get;  set; } = new List<Patient>();
        public ICollection<PracticePractitioner> PracticePractitioners { get; set; } = new List<PracticePractitioner>();
    }
}
