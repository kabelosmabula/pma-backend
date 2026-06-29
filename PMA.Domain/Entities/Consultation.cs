using PMA.Domain.Enums;

namespace PMA.Domain.Entities
{
    public class Consultation : BaseEntity
    {
        public Guid ClinicalRecordId { get; set; }
        public Guid PracticePractitionerId { get; set; }
        public Guid? AppointmentId { get; set; }
        public Guid? OrganisationId { get; set; }
        public string PatientId { get; set; }
        public DateTime VisitDate { get; set; }
        public string ConsultationNotes { get; set; }
        public SpecialtyType SpecialtyTypes { get; set; }
        public ClinicalRecord ClinicalRecord { get; set; }
        public string reasonforvisit { get; set; }
        public BillingType BillingType { get; set; }
        public ConsultationStatus status { get; set; }
        public bool RequiresFollowUp { get; set; }
        public Guid? ParentConsultationId { get; set; }
        public PracticePractitioner PracticePractitioner { get; set; }
        public Appointment Appointment { get; set; }
        public Vital Vital { get; set; }
        public CardiologyConsultation CardiologyConsultation { get; set; }
        public DermatologyConsultation DermatologyConsultation { get; set; }
        public PsychiatryConsultation PsychiatryConsultation { get; set; }
        public PediatricsConsultation PediatricsConsultation { get; set; }
        public OncologyConsultation OncologyConsultation { get; set; }
        public DentistryConsultation DentistryConsultation { get; set; }
        public ICollection<PatientReferral> Refferals { get; set; } = new List<PatientReferral>();
        public ICollection<Diagnosis> Diagnoses { get; set; } = new List<Diagnosis>();
        public ICollection<Procedure> Procedures { get; set; } = new List<Procedure>();
        public ICollection<ClinicalDocument> Documents { get; set; } =new List<ClinicalDocument>();
        public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
        public Invoice? Invoice { get; set; }
    }

   
}
