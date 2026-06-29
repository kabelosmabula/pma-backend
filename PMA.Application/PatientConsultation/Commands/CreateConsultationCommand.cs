using AutoMapper;
using Common;
using MediatR;
using PMA.Application.Interface.Mapping;
using PMA.Application.PatientAppointments.Commands;
using PMA.Application.PatientConsultation.DTOs;
using PMA.Domain.Entities;
using PMA.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.PatientConsultation.Commands
{
    public class CreateConsultationCommand : IRequest<Result<string>>
    {
        public BillingType BillingType { get; set; }
        public string appointmentId { get; set; }
        public string patientId { get; set; }
        public string practicePractitionerId { get; set; }
        public string reasonForVisit { get; set; }
        public string consultationNotes { get; set; }
        public string status { get; set; }
        public bool isfollowUpRequired { get; set; }
        public DateTime? FollowUpDate { get; set; }
        public DateTime? FollowUpStartTime { get; set; }
        public DateTime? FollowUpEndTime { get; set; }
        public string FollowUpReason { get; set; }
        public SpecialtyType SpecialtyType { get; set; }
        public DateTime visitDate { get; set; }
        public VitalDto vitals { get; set; }
        public List<DiagnosisDto> diagnoses { get; set; }
        public List<proceduresDto> procedures { get; set; }
        public List<prescriptionsDto> prescriptions { get; set; }
        public List<clinicalDocumentsDto> clinicalDocuments { get; set; }
        public CardiologyConsultationDto CardiologyData { get; set; }
        public DermatologyConsultationDto DermatologyData { get; set; }
        public PsychiatryConsultationDto PsychiatryData { get; set; }
        public PediatricsConsultationDto PediatricsData { get; set; }
        public OncologyConsultationDto OncologyData { get; set; }
        public DentistryConsultationDto DentistryData { get; set; }
    }
}
