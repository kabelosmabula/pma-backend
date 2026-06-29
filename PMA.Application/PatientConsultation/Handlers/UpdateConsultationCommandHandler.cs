using Common;
using DocumentFormat.OpenXml.Office2016.Excel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMA.Application.PatientConsultation.Commands;
using PMA.Application.PatientConsultation.DTOs;
using PMA.Domain.Entities;
using PMA.Domain.Enums;
using PMA.Persistence;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PMA.Application.PatientConsultation.Handlers
{
    public class UpdateConsultationCommandHandler : IRequestHandler<UpdateConsultationCommand, Result<string>>
    {
        private readonly PMADBContext _context;

        public UpdateConsultationCommandHandler(PMADBContext context)
        {
            _context = context;
        }

        public async Task<Result<string>> Handle(UpdateConsultationCommand request, CancellationToken cancellationToken)
        {
            using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                var consultation = await _context.Consultations
                    .Include(c => c.Appointment)
                    .FirstOrDefaultAsync(x => x.id == Guid.Parse(request.ConsultationId) && !x.isdeleted, cancellationToken);

                if (consultation == null)
                    return Result<string>.Fail("Consultation not found");

                UpdateMainConsultation(consultation, request);

                if (request.Vitals != null)
                {
                    await UpdateVitals(consultation, request.Vitals, cancellationToken);
                }

                if (request.Diagnoses != null)
                {
                    await UpdateDiagnoses(consultation, request.Diagnoses, cancellationToken);
                }

                if (request.Procedures != null)
                {
                    await UpdateProcedures(consultation, request.Procedures, cancellationToken);
                }

                if (request.Prescriptions != null)
                {
                    await UpdatePrescriptions(consultation, request.Prescriptions, cancellationToken);
                }

                if (request.ClinicalDocuments != null)
                {
                    await UpdateClinicalDocuments(consultation, request.ClinicalDocuments, cancellationToken);
                }
                if (consultation.SpecialtyTypes != SpecialtyType.None)
                {
                    var specialtyUpdateResult = await UpdateSpecialtyConsultation(request.SpecialtyTypes, consultation.id,request,cancellationToken);
                    
                    if (!specialtyUpdateResult.Success)
                        return Result<string>.Fail(specialtyUpdateResult.Error);
                }
                consultation.modifiedby = consultation.createdby;
                if (request.isfollowUpRequired)
                {

                    if (consultation.Appointment?.PractitionerId == null)
                        return Result<string>.Fail("Original appointment has no valid practitioner.");

                    if (consultation.Appointment == null)
                        return Result<string>.Fail("Consultation has no linked appointment.");

                    if (!consultation.OrganisationId.HasValue)
                        return Result<string>.Fail("OrganisationId is missing.");

                    if (!Guid.TryParse(consultation.PatientId, out var patientGuid))
                        return Result<string>.Fail("Invalid PatientId.");

                    if (!request.FollowUpDate.HasValue)
                        return Result<string>.Fail("Follow-up appointment date is required.");

                    if (!request.FollowUpStartTime.HasValue)
                        return Result<string>.Fail("Follow-up start time is required.");

                    if (!request.FollowUpEndTime.HasValue)
                        return Result<string>.Fail("Follow-up end time is required.");

                    if (request.FollowUpStartTime >= request.FollowUpEndTime)
                        return Result<string>.Fail("Invalid follow-up appointment times.");

                    var overlap = await _context.Appointments.AnyAsync(x =>
                        x.PractitionerId == consultation.Appointment.PractitionerId &&
                        x.AppointmentDate == DateOnly.FromDateTime(request.FollowUpDate.Value) &&
                        !x.isdeleted &&
                        (
                            request.FollowUpStartTime < x.endappointment &&
                            request.FollowUpEndTime > x.startappointment
                        ),
                        cancellationToken);

                    if (overlap)
                        return Result<string>.Fail("Doctor already has a follow-up appointment during that time.");
                    var followUpAppointment = new Appointment
                    {
                        Appointmentreference = "FU-followup check up",
                        Appointmenttype = "FollowUp",
                        AppointmentDate = DateOnly.FromDateTime(request.FollowUpDate.Value),
                        PatientId = Guid.Parse(consultation.PatientId),
                        Priority = "check-up",
                        IsFollowUp = request.isfollowUpRequired,
                        HouseholdId = consultation.OrganisationId.Value,
                        PracticeId = consultation.Appointment.PracticeId,
                        PractitionerId = consultation.Appointment.PractitionerId,
                        startappointment = request.FollowUpStartTime.Value, 
                        endappointment = request.FollowUpEndTime.Value,
                        Status = AppointmentStatus.Booked,
                        createddate = DateTime.UtcNow,
                        createdby = consultation.createdby
                    };
                    await _context.Appointments.AddAsync(followUpAppointment, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);
                    Console.WriteLine($"AppointmentId: {followUpAppointment.id}");

                    var followUpConsultation = new Consultation
                    {
                        ClinicalRecordId = consultation.ClinicalRecordId,
                        AppointmentId = followUpAppointment.id,
                        ParentConsultationId = consultation.id,
                        PracticePractitionerId = consultation.PracticePractitionerId,
                        PatientId = consultation.PatientId.ToString(),
                        VisitDate = DateTime.UtcNow,
                        RequiresFollowUp = request.isfollowUpRequired,
                        ConsultationNotes = string.Empty,
                        reasonforvisit = request.FollowUpReason,
                        BillingType = consultation.BillingType,
                        SpecialtyTypes = consultation.SpecialtyTypes,
                        OrganisationId = consultation.OrganisationId,
                        status = ConsultationStatus.InProgress,
                        createddate = DateTime.UtcNow,
                        createdby = consultation.createdby
                    };

                    await _context.Consultations.AddAsync(followUpConsultation, cancellationToken);

                }
                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                return Result<string>.Ok("Consultation updated successfully");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                return Result<string>.Fail($"An error occurred while updating consultation.{ex.Message}");
            }
        }

        private void UpdateMainConsultation(Consultation consultation, UpdateConsultationCommand request)
        {
            if (!string.IsNullOrEmpty(request.ConsultationNotes))
                consultation.ConsultationNotes = request.ConsultationNotes;

            if (!string.IsNullOrEmpty(request.ReasonForVisit))
                consultation.reasonforvisit = request.ReasonForVisit;

            if (request.BillingType.HasValue)
                consultation.BillingType = request.BillingType.Value;

            consultation.modifieddate = DateTime.UtcNow;
        }

        private async Task UpdateVitals(Consultation consultation, VitalDto vitalsDto, CancellationToken cancellationToken)
        {
            var vital = await _context.Vitals
                .FirstOrDefaultAsync(x => x.consultationId == consultation.id, cancellationToken);

            if (vital != null)
            {
                decimal bmi = 0;

                vital.Temperature = vitalsDto.Temperature;
                vital.Pulse = vitalsDto.Pulse;
                vital.oxygenSaturation = vitalsDto.oxygenSaturation;
                vital.respiratoryRate = vitalsDto.respiratoryRate;
                vital.SystolicBP = vitalsDto.SystolicBP;
                vital.DiastolicBP = vitalsDto.DiastolicBP;
                vital.Weight = vitalsDto.Weight;
                vital.height = vitalsDto.height;
                vital.modifieddate = DateTime.UtcNow;
                vital.modifiedby = vital.createdby;
                vital.createdby = consultation.createdby;

                if (vital.height > 0)
                {
                    var heightInMeters = vital.height / 100m;
                    bmi = (decimal)((vital.Weight) / (heightInMeters * heightInMeters));
                }
                vital.BMI = bmi;
            }
            else
            {
                var newVital = new Vital
                {
                    consultationId = consultation.id,
                    Temperature = vitalsDto.Temperature,
                    Pulse = vitalsDto.Pulse,
                    oxygenSaturation = vitalsDto.oxygenSaturation,
                    respiratoryRate = vitalsDto.respiratoryRate,
                    SystolicBP = vitalsDto.SystolicBP,
                    DiastolicBP = vitalsDto.DiastolicBP,
                    Weight = vitalsDto.Weight,
                    height = vitalsDto.height,
                    createddate = DateTime.UtcNow,
                    createdby = consultation.createdby,
                };

                if (vitalsDto.height > 0)
                {
                    var heightInMeters = vitalsDto.height / 100m;
                    newVital.BMI = (vitalsDto.Weight) / (heightInMeters * heightInMeters);
                }

                await _context.Vitals.AddAsync(newVital, cancellationToken);
            }
        }

        private async Task UpdateDiagnoses(Consultation consultation, List<DiagnosisDto> diagnoses, CancellationToken cancellationToken)
        {
            var existingDiagnoses = _context.Diagnoses.Where(x => x.ConsultationId == consultation.id);
            _context.Diagnoses.RemoveRange(existingDiagnoses);

            foreach (var diag in diagnoses)
            {
                var diagnosis = new Diagnosis
                {
                    ConsultationId = consultation.id,
                    Code = diag.Code,
                    Description = diag.Description,
                    createddate = DateTime.UtcNow,
                    createdby = consultation.createdby
                };
                await _context.Diagnoses.AddAsync(diagnosis, cancellationToken);
            }
        }

        private async Task UpdateProcedures(Consultation consultation, List<proceduresDto> procedures, CancellationToken cancellationToken)
        {
            var existingProcedures = _context.Procedures.Where(x => x.ConsultationId == consultation.id);
            _context.Procedures.RemoveRange(existingProcedures);

            foreach (var proc in procedures)
            {
                var procedure = new Procedure
                {
                    ConsultationId = consultation.id,
                    code = proc.code,
                    name = proc.name,
                    description = proc.description,
                    tariffAmount = proc.tariffAmount,
                    createddate = DateTime.UtcNow,
                    createdby = consultation.createdby
                };
                await _context.Procedures.AddAsync(procedure, cancellationToken);
            }
        }

        private async Task UpdatePrescriptions(Consultation consultation, List<prescriptionsDto> prescriptions, CancellationToken cancellationToken)
        {
            var existingPrescriptions = _context.Prescriptions.Where(x => x.consultationId == consultation.id);
            _context.Prescriptions.RemoveRange(existingPrescriptions);

            foreach (var pres in prescriptions)
            {
                var prescription = new Prescription
                {
                    consultationId = consultation.id,
                    medicationName = pres.medicationName,
                    frequency = pres.frequency,
                    duration = pres.duration,
                    notes = pres.notes,
                    dosage = pres.dosage,
                    createddate = DateTime.UtcNow,
                    createdby = consultation.createdby
                };
                await _context.Prescriptions.AddAsync(prescription, cancellationToken);
            }
        }
        private async Task UpdateClinicalDocuments(Consultation consultation, List<clinicalDocumentsDto> documents, CancellationToken cancellationToken)
        {
            foreach (var doc in documents)
            {
                var existingDoc = await _context.ClinicalDocuments
                    .FirstOrDefaultAsync(x => x.ConsultationId == consultation.id && x.documentName == doc.documentName, cancellationToken);

                if (existingDoc != null)
                {
                    existingDoc.documentType = doc.documentType ?? existingDoc.documentType;
                    existingDoc.fileUrl = doc.fileUrl ?? existingDoc.fileUrl;
                    existingDoc.modifieddate = DateTime.UtcNow;
                    existingDoc.modifiedby = consultation.createdby;
                }
                else
                {
                    var clinicalDocument = new ClinicalDocument
                    {
                        documentName = doc.documentName ?? " ",
                        documentType = doc.documentType ?? " ",
                        fileUrl = doc.fileUrl ?? " ",
                        ConsultationId = consultation.id,
                        createddate = DateTime.UtcNow,
                        createdby = consultation.createdby
                    };
                    await _context.ClinicalDocuments.AddAsync(clinicalDocument, cancellationToken);
                }
            }
        }

        private async Task<Result<bool>> UpdateSpecialtyConsultation(SpecialtyType specialty,Guid consultationId,UpdateConsultationCommand request,CancellationToken cancellationToken)
        {
            switch (specialty)
            {
                case SpecialtyType.Cardiology:
                    return await UpdateCardiologyConsultation(consultationId, request, cancellationToken);

                case SpecialtyType.Dermatology:
                    return await UpdateDermatologyConsultation(consultationId, request, cancellationToken);

                case SpecialtyType.Psychiatry:
                    return await UpdatePsychiatryConsultation(consultationId, request, cancellationToken);

                case SpecialtyType.Pediatrics:
                    return await UpdatePediatricsConsultation(consultationId, request, cancellationToken);

                case SpecialtyType.Oncology:
                    return await UpdateOncologyConsultation(consultationId, request, cancellationToken);

                case SpecialtyType.Dentistry:
                    return await UpdateDentistryConsultation(consultationId, request, cancellationToken);

                case SpecialtyType.None:
                    return Result<bool>.Ok(true);

                default:
                    return Result<bool>.Fail($"Unknown specialty: {specialty}");
            }
        }

        private async Task<Result<bool>> UpdateCardiologyConsultation(Guid consultationId,UpdateConsultationCommand request, CancellationToken cancellationToken)
        {
            CardiologyConsultationDto data = request.CardiologyData;
            if (data == null)
                return Result<bool>.Ok(true);

            var cardiology = await _context.CardiologyConsultations
                .FirstOrDefaultAsync(x => x.ConsultationId == consultationId, cancellationToken);

            if (cardiology == null)
            {
                 cardiology = new CardiologyConsultation
                {
                    ConsultationId = consultationId,
                    createdby = request.userid
                };
                await _context.CardiologyConsultations.AddAsync(cardiology, cancellationToken);
            }
            else
            {
                cardiology.modifieddate = DateTime.UtcNow;
                cardiology.modifiedby = request.userid;
            }

            cardiology.ECGPerformed = data.ECGPerformed;
            cardiology.ECGFindings = data.ECGFindings;
            cardiology.EchoPerformed = data.EchoPerformed;
            cardiology.EjectionFraction = data.EjectionFraction;
            cardiology.SmokingRisk = data.SmokingRisk;
            cardiology.HypertensionRisk = data.HypertensionRisk;

            return Result<bool>.Ok(true);
        }

        private async Task<Result<bool>> UpdateDermatologyConsultation(Guid consultationId, UpdateConsultationCommand request ,CancellationToken cancellationToken)
        {
            DermatologyConsultationDto data = request.DermatologyData;
            if (data == null)
                return Result<bool>.Ok(true);

            var dermatology = await _context.DermatologyConsultations
                .Include(x => x.ClinicalImages)
                .FirstOrDefaultAsync(x => x.ConsultationId == consultationId, cancellationToken);

            if (dermatology == null)
            {
                dermatology = new DermatologyConsultation
                {
                    ConsultationId = consultationId,
                    createdby = request.userid
                };
                await _context.DermatologyConsultations.AddAsync(dermatology, cancellationToken);
               
            }
            else
            {
                dermatology.modifieddate = DateTime.UtcNow;
                dermatology.modifiedby = request.userid;
            }

            dermatology.SkinCondition = data.SkinCondition ?? dermatology.SkinCondition;
            dermatology.Severity = data.Severity ?? dermatology.Severity;
            dermatology.AffectedAreas = data.AffectedAreas ?? dermatology.AffectedAreas;
            await _context.SaveChangesAsync(cancellationToken);
            if (data.ClinicalImages != null && data.ClinicalImages.Any())
            {
                _context.ClinicalImages.RemoveRange(dermatology.ClinicalImages);

                dermatology.ClinicalImages = data.ClinicalImages.Select(img => new ClinicalImage
                {
                    ImageUrl = img.ImageUrl,
                    Description = img.Description,
                    CapturedAt = DateTime.UtcNow,
                    DermatologyConsultationId = dermatology.id,
                    createdby = request.userid
                }).ToList();
            }
            return Result<bool>.Ok(true);
        }
        private async Task<Result<bool>> UpdatePsychiatryConsultation(Guid consultationId, UpdateConsultationCommand request, CancellationToken cancellationToken)
        {
            PsychiatryConsultationDto data = request.PsychiatryData;
            if (data == null)
                return Result<bool>.Ok(true);

            var psychiatry = await _context.PsychiatryConsultations
                .FirstOrDefaultAsync(x => x.ConsultationId == consultationId, cancellationToken);

            if (psychiatry == null)
            {
                psychiatry = new PsychiatryConsultation
                {
                    ConsultationId = consultationId,
                    createdby = request.userid
                };
                await _context.PsychiatryConsultations.AddAsync(psychiatry, cancellationToken);
            }
            else
            {
                psychiatry.modifieddate = DateTime.UtcNow;
                psychiatry.modifiedby = request.userid;
            }

            psychiatry.Mood = data.Mood ?? psychiatry.Mood;
            psychiatry.Affect = data.Affect ?? psychiatry.Affect;
            psychiatry.Speech = data.Speech ?? psychiatry.Speech;
            psychiatry.SuicidalIdeation = data.SuicidalIdeation;
            psychiatry.SelfHarmRiskLevel = data.SelfHarmRiskLevel ?? psychiatry.SelfHarmRiskLevel;
            psychiatry.TherapyNotes = data.TherapyNotes ?? psychiatry.TherapyNotes;

            return Result<bool>.Ok(true);
        }
        private async Task<Result<bool>> UpdatePediatricsConsultation(Guid consultationId,  UpdateConsultationCommand request, CancellationToken cancellationToken)
        {
            PediatricsConsultationDto data = request.PediatricsData;
            if (data == null)
                return Result<bool>.Ok(true);

            var pediatrics = await _context.PediatricsConsultations
                .FirstOrDefaultAsync(x => x.ConsultationId == consultationId, cancellationToken);

            if (pediatrics == null)
            {
                pediatrics = new PediatricsConsultation
                {
                    ConsultationId = consultationId,
                    createdby = request.userid
                };
                await _context.PediatricsConsultations.AddAsync(pediatrics, cancellationToken);
            }
            else
            {
                pediatrics.modifieddate = DateTime.UtcNow;
                pediatrics.modifiedby = request.userid;
            }

            pediatrics.WeightKg = data.WeightKg != 0 ? data.WeightKg : pediatrics.WeightKg;
            pediatrics.HeightCm = data.HeightCm != 0 ? data.HeightCm : pediatrics.HeightCm;
            pediatrics.VaccinationsUpToDate = data.VaccinationsUpToDate;
            pediatrics.DevelopmentalMilestones = data.DevelopmentalMilestones ?? pediatrics.DevelopmentalMilestones;

            return Result<bool>.Ok(true);
        }
        private async Task<Result<bool>> UpdateOncologyConsultation(Guid consultationId, UpdateConsultationCommand request, CancellationToken cancellationToken)
        {
            OncologyConsultationDto data = request.OncologyData;
            if (data == null)
                return Result<bool>.Ok(true);

            var oncology = await _context.OncologyConsultations
                .FirstOrDefaultAsync(x => x.ConsultationId == consultationId, cancellationToken);

            if (oncology == null)
            {
                oncology = new OncologyConsultation
                {
                    ConsultationId = consultationId,
                    createdby = request.userid
                };
                await _context.OncologyConsultations.AddAsync(oncology, cancellationToken);
            }
            else
            {
                oncology.modifieddate = DateTime.Now;
                oncology.modifiedby = request.userid;
            }

            oncology.CancerType = data.CancerType ?? oncology.CancerType;
            oncology.CancerStage = data.CancerStage ?? oncology.CancerStage;
            oncology.ChemotherapyCycle = data.ChemotherapyCycle != 0 ? data.ChemotherapyCycle : oncology.ChemotherapyCycle;
            oncology.TumorResponse = data.TumorResponse ?? oncology.TumorResponse;

            return Result<bool>.Ok(true);
        }

        private async Task<Result<bool>> UpdateDentistryConsultation(Guid consultationId, UpdateConsultationCommand request, CancellationToken cancellationToken)
        {
            DentistryConsultationDto data = request.DentistryData ?? new DentistryConsultationDto();
            if (data == null)
                return Result<bool>.Ok(true);

            var dentistry = await _context.DentistryConsultations
                .Include(x => x.ToothCharts)
                .FirstOrDefaultAsync(x => x.ConsultationId == consultationId, cancellationToken);

            if (dentistry == null)
            {
                dentistry = new DentistryConsultation
                {
                    ConsultationId = consultationId,
                    createdby = request.userid
                };
                await _context.DentistryConsultations.AddAsync(dentistry, cancellationToken);
            }
            else
            {
                dentistry.modifieddate = DateTime.Now;
                dentistry.modifiedby = request.userid;
            }

            dentistry.XRayCompleted = data.XRayCompleted;
            dentistry.GumCondition = data.GumCondition ?? dentistry.GumCondition;
            await _context.SaveChangesAsync(cancellationToken);


            if (data.ToothCharts != null && data.ToothCharts.Any())
            {
                _context.ToothCharts.RemoveRange(dentistry.ToothCharts);


                dentistry.ToothCharts = data.ToothCharts.Select(tooth => new ToothChart
                {
                    ToothNumber = tooth.ToothNumber,
                    Condition = tooth.Condition,
                    Notes = tooth.Notes,
                    DentistryConsultationId = dentistry.id,
                    createdby = request.userid
                }).ToList();
            }

            return Result<bool>.Ok(true);
        }
    }
}
