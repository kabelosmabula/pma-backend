using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Domain.Entities
{
    public class PracticePractitioner : BaseEntity
    {
        public Guid PracticeId { get; set; }
        public Guid PractitionerId { get; set; }
        public string Specialty { get; set; } = null!;
        public string PracticeEmail { get; set; } = null!;
        public bool Isactive { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Practice Practice { get; set; }
        public Practitioner Practitioner { get; set; }
        public ICollection<PractitionerAvailability> Availabilities { get; set; } = new List<PractitionerAvailability>();

    }
}

