using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Domain.Entities
{
    public class PractitionerAvailability : BaseEntity
    {
        public Guid PracticePractitionerId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public PracticePractitioner PracticePractitioner { get; set; }
    }
}
