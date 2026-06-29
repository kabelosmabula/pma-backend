using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Domain.Entities
{
    public class Practitioner : BaseEntity
    {
        public Guid UserId { get;  set; }
        public string HPCSANumber { get; set; }
        public string Fullname { get; set; } 
        public int Yearsofexperience { get; set; }
        public DateOnly Licenseexpirydate { get; set; }
        public User User { get; private set; }
        public ICollection<PracticePractitioner> PracticePractitioners { get; set; } = new List<PracticePractitioner>();
        
    }
}
