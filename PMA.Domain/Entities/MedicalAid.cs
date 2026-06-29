using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Domain.Entities
{
    public class MedicalAid : BaseEntity
    {
        public Guid PatientId { get; set; }
        public string SchemeCode { get; set; }
        public string SchemeName { get; set; }
        public string PlanCode { get; set; }
        public string PlanName { get; set; }
        public string MembershipNumber { get; set; }
        public string DependentCode { get; set; }
        public bool IsActive { get; set; }
        public DateOnly? EffectiveFrom { get; set; }
        public DateOnly? EffectiveTo { get; set; }
        public Patient Patient { get; set; }
    }
}
