using PMA.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace PMA.Domain.Entities
{
    public class Appointment : BaseEntity
    {
        public string Appointmentreference { get; set; } 
        public string Appointmenttype { get; set; } = null!;
        public string Priority { get; set; } = null!;
        public Guid PracticeId { get; set; }
        public Guid PatientId { get; set; }
        public Guid PractitionerId { get; set; }
        public Guid HouseholdId { get; set; }
        public bool IsFollowUp { get; set; }
        public DateOnly AppointmentDate { get; set; }
        public DateTime startappointment { get; set; }
        public DateTime endappointment { get; set; }
        public AppointmentStatus Status { get; set; }
        public Practice Practice { get; set; }
        public Patient Patient { get; set; }
        [ForeignKey(nameof(HouseholdId))]
        public PatientHousehold PatientHousehold { get; set; }
        public PracticePractitioner Practitioner { get; set; }
    }
}
