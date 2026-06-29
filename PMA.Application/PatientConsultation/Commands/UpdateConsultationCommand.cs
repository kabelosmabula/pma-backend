using Common;
using MediatR;
using PMA.Application.PatientConsultation.DTOs;
using PMA.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.PatientConsultation.Commands
{
    public class UpdateConsultationCommand : IRequest<Result<string>>
    {
        public string ConsultationId { get; set; }
        public string userid { get; set; }
        public string ConsultationNotes { get; set; }
        public string ReasonForVisit { get; set; }
        public SpecialtyType SpecialtyTypes { get; set; }
        public BillingType? BillingType { get; set; }

        public bool isfollowUpRequired { get; set; }
        public DateTime? FollowUpDate { get; set; }
        public DateTime? FollowUpStartTime { get; set; }
        public DateTime? FollowUpEndTime { get; set; }
        public string FollowUpReason { get; set; }
        public Guid? ParentConsultationId { get; set; }

        public VitalDto Vitals { get; set; }
        public List<DiagnosisDto> Diagnoses { get; set; }
        public List<proceduresDto> Procedures { get; set; }
        public List<prescriptionsDto> Prescriptions { get; set; }
        public List<clinicalDocumentsDto> ClinicalDocuments { get; set; }

        public CardiologyConsultationDto CardiologyData { get; set; }
        public DermatologyConsultationDto DermatologyData { get; set; }
        public PsychiatryConsultationDto PsychiatryData { get; set; }
        public PediatricsConsultationDto PediatricsData { get; set; }
        public OncologyConsultationDto OncologyData { get; set; }
        public DentistryConsultationDto DentistryData { get; set; }
    }

}
