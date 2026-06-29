using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Domain.Entities
{
    public class Patient : BaseEntity
    {
        public Guid PracticeId { get; set; }
        public Practice Practice { get; set; }
        public Guid HouseholdId { get;  set; }
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string Identitynumber { get; set; }
        public string Displayname { get; set; }
        public string Phonenumber { get; set; } 
        public string Gender { get; set; } 
        public DateOnly? Dateofbirth { get; set; }
        public Guid? MedicalAidId { get; set; }
        public  ClinicalRecord ClinicalRecord { get; set; }
        public ICollection<MedicalAid> MedicalAids { get; set; } = new List<MedicalAid>();
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<Allergy> Allergies { get; set; } = new List<Allergy>();
        public ICollection<Address> Addresses { get; set; } = new List<Address>();
        public ICollection<EmergencyContact> EmergencyContacts { get; set; } = new List<EmergencyContact>();
        public ICollection<PatientHousehold> Households { get; set; }
        public string Email { get; set; }
    }
}
