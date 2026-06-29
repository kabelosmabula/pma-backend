using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Domain.Entities
{
    public class PatientHousehold : BaseEntity
    {
        public Guid PracticeId { get;  set; }
        public Guid? MedicalAidId { get; set; }
        public Practice Practice { get;  set; }
        public string HouseholdName { get;  set; } 
        public string PrimaryContactName { get;  set; }
        public string PrimaryContactSurName { get;  set; }
        public string PrimaryContactPhone { get;  set; }
        public string Email { get;  set; }
        public string? Address { get; set; }
        public ICollection<Patient> Patients { get;  set; } = new List<Patient>();
        public ICollection<HouseHoldInvoiceCollection> HouseHoldInvoice { get; set; } = new List<HouseHoldInvoiceCollection>();
    }
}
