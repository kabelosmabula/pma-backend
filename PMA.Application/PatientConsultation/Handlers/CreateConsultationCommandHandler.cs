using Common;
using DocumentFormat.OpenXml.Wordprocessing;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMA.Application.PatientConsultation.Commands;
using PMA.Domain.Entities;
using PMA.Domain.Enums;
using PMA.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.PatientConsultation.Handlers
{
    public class CreateConsultationCommandHandler : IRequestHandler<CreateConsultationCommand, Result<string>>
    {
        private readonly PMADBContext _context;

        public CreateConsultationCommandHandler(PMADBContext context)
        {
            _context = context;
        }

        public async Task<Result<string>> Handle(CreateConsultationCommand request, CancellationToken cancellationToken)
        {
            using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                var record = await _context.ClinicalRecords
                    .FirstOrDefaultAsync(x => x.PatientId == Guid.Parse(request.patientId) && !x.isdeleted, cancellationToken);
                if (record == null)
                    return Result<string>.Fail("Patient clinical record not found");
                var practitioner = await _context.PracticePractitioners
                    .FirstOrDefaultAsync(x => x.id == Guid.Parse(request.practicePractitionerId) && !x.isdeleted, cancellationToken);
                if (practitioner == null)
                    return Result<string>.Fail("Practitioner not found");

                var appointmentconfimation = await _context.Appointments
                    .FirstOrDefaultAsync(x => (x.id == Guid.Parse(request.appointmentId) && (x.Status == AppointmentStatus.Booked) && (!x.isdeleted)), cancellationToken);

                if(appointmentconfimation == null)
                    return Result<string>.Fail("Appointment is not Booked.");

                var consultation = new Consultation
                {
                    ClinicalRecordId = record.id,
                    PracticePractitionerId = Guid.Parse(request.practicePractitionerId),
                    AppointmentId =  appointmentconfimation.id,
                    PatientId = record.PatientId.ToString(),
                    VisitDate = DateTime.UtcNow,
                    ConsultationNotes = request.consultationNotes,
                    reasonforvisit = request.reasonForVisit,
                    BillingType = request.BillingType,
                    SpecialtyTypes = request.SpecialtyType,
                    OrganisationId = appointmentconfimation.HouseholdId,
                    createddate = DateTime.UtcNow,
                    createdby = request.practicePractitionerId
                };
                Console.WriteLine($"Appointment ID: {appointmentconfimation.id}");
                await _context.Consultations.AddAsync(consultation, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                if (consultation.SpecialtyTypes != null)
                {
                    var specialtyResult = await CreateSpecialtyConsultation(request, consultation.id, cancellationToken);
                    if (!specialtyResult.Success)
                        return Result<string>.Fail(specialtyResult.Error);
                }
                decimal bmi = 0;

                if (request.vitals.height > 0)
                {
                    var heightInMeters = request.vitals.height / 100;

                    bmi = request.vitals?.Weight?? 0 /
                          (heightInMeters?? 0 * heightInMeters?? 0);
                }
                var vital = new Vital
                {
                    consultationId = consultation.id,
                    Temperature = request.vitals?.Temperature ?? 0,
                    Pulse = request.vitals?.Pulse ?? 0,
                    oxygenSaturation = request.vitals?.oxygenSaturation ?? 0,
                    respiratoryRate = request.vitals?.respiratoryRate ?? 0,
                    SystolicBP = request.vitals?.SystolicBP ?? 0,
                    DiastolicBP = request.vitals?.DiastolicBP ?? 0,
                    Weight = request.vitals?.Weight ?? 0,
                    height = request.vitals?.height?? 0,
                    BMI = bmi,
                    createddate = DateTime.UtcNow,
                    createdby = request.practicePractitionerId
                };
                await _context.Vitals.AddAsync(vital, cancellationToken);
                if (request.diagnoses != null && request.diagnoses.Any())
                {
                    foreach (var diag in request.diagnoses)
                    {
                        var diagnosis = new Diagnosis
                        {
                            ConsultationId = consultation.id,
                            Code = diag.Code,
                            Description = diag.Description,
                            createdby = request.practicePractitionerId.ToString(),
                        };

                        await _context.Diagnoses.AddAsync(diagnosis,cancellationToken);
                    }
                }
                decimal totalAmount = 0;
                if (request.procedures != null && request.procedures.Any())
                {
                    foreach (var proc in request.procedures)
                    {
                        var procedure = new Procedure
                        {
                            ConsultationId = consultation.id,
                            code = proc.code,
                            name = proc.name,
                            description = proc.description,
                            tariffAmount = proc.tariffAmount,
                            createdby = request.practicePractitionerId.ToString()
                        };

                        totalAmount += proc.tariffAmount;
                        await _context.Procedures.AddAsync(procedure, cancellationToken);
                    }
                }
                if (request.prescriptions != null && request.prescriptions.Any())
                {
                    foreach (var pres in request.prescriptions)
                    {
                        var prescription = new Prescription
                        {
                            consultationId = consultation.id,
                            medicationName = pres.medicationName,
                            frequency = pres.frequency,
                            duration = pres.duration,
                            notes = pres.notes,
                            dosage = pres.dosage,
                            createdby = request.practicePractitionerId.ToString(),
                        };

                        await _context.Prescriptions.AddAsync(prescription, cancellationToken);
                    }
                }
                if (request.clinicalDocuments != null && request.clinicalDocuments.Any())
                {
                    foreach (var cdocs in request.clinicalDocuments)
                    {
                        var clinicalDocuments = new ClinicalDocument
                        {
                            documentName = cdocs.documentName?? " ",
                            documentType = cdocs.documentType?? " ",
                            fileUrl = cdocs.fileUrl ?? " ",
                            ConsultationId = consultation.id,
                            createddate = DateTime.UtcNow,
                            createdby = request.practicePractitionerId.ToString(),
                        };

                        await _context.ClinicalDocuments.AddAsync(clinicalDocuments, cancellationToken);
                    }
                }

                var invoice = new Invoice
                {
                    ConsultationId= consultation.id,
                    TotalAmount = totalAmount,
                    Status = request.BillingType == BillingType.Cash ? "Paid" : "Pending",
                    createddate = DateTime.UtcNow,
                    createdby = request.practicePractitionerId.ToString(),
                    LineItems = new List<InvoiceLineItem>()
                };
                decimal consultationFee = 500; 
                invoice.LineItems.Add(new InvoiceLineItem
                {
                    ReferenceCode = "CONSULT",
                    Description = "Consultation Fee",
                    Amount = consultationFee,
                    createdby = request.practicePractitionerId.ToString(),
                });
                totalAmount += consultationFee;
                if (request.procedures != null && request.procedures.Any())
                {
                    foreach (var proc in request.procedures)
                    {
                        invoice.LineItems.Add(new InvoiceLineItem
                        {
                            ReferenceCode = proc.code,
                            Description = proc.description,
                            Amount = proc.tariffAmount,
                            createdby = request.practicePractitionerId.ToString()
                        });
                        totalAmount += proc.tariffAmount;
                    }
                }
                if (consultation.BillingType == BillingType.Household)
                {
                    var patient = await _context.Patients
                        .FirstOrDefaultAsync(x => x.id == Guid.Parse(request.patientId), cancellationToken);

                    if (patient != null)
                    {
                        invoice.PatientMedicalAidId = patient.MedicalAidId;
                    }
                    invoice.Status = "Pending";
                    invoice.createddate = DateTime.UtcNow;
                    invoice.createdby = request.practicePractitionerId.ToString();
                }
                else if (consultation.BillingType == BillingType.Cash)
                {
                    invoice.Status = "Paid";
                    invoice.createddate = DateTime.UtcNow;
                    invoice.createdby = request.practicePractitionerId.ToString();
                }
                invoice.TotalAmount = totalAmount;
                await _context.Invoices.AddAsync(invoice, cancellationToken);

                if (request.isfollowUpRequired)
                {

                    if (!request.FollowUpDate.HasValue)
                        return Result<string>.Fail("Follow-up appointment date is required.");

                    if (!request.FollowUpStartTime.HasValue)
                        return Result<string>.Fail("Follow-up start time is required.");

                    if (!request.FollowUpEndTime.HasValue)
                        return Result<string>.Fail("Follow-up end time is required.");

                    if (request.FollowUpStartTime >= request.FollowUpEndTime)
                        return Result<string>.Fail("Invalid follow-up appointment times.");

                    var overlap = await _context.Appointments.AnyAsync(x =>
                        x.PractitionerId == Guid.Parse(request.practicePractitionerId) &&
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
                        Appointmentreference = $"FU-{DateTime.UtcNow:yyyyMMddHHmmss}",
                        Appointmenttype = "FollowUp",
                        AppointmentDate = DateOnly.FromDateTime(request.FollowUpDate.Value),
                        PatientId = Guid.Parse(request.patientId),
                        Priority = "check-up",
                        IsFollowUp = request.isfollowUpRequired,
                        HouseholdId = appointmentconfimation.HouseholdId,
                        PracticeId = appointmentconfimation.PracticeId,
                        PractitionerId = Guid.Parse(request.practicePractitionerId),
                        startappointment = request.FollowUpStartTime.Value,
                        endappointment = request.FollowUpEndTime.Value,
                        Status = AppointmentStatus.Booked,
                        createddate = DateTime.UtcNow,
                        createdby = request.practicePractitionerId
                    };
                    await _context.Appointments.AddAsync(followUpAppointment, cancellationToken);
                    Console.WriteLine($"AppointmentId: {followUpAppointment.id}");
                   
                    var followUpConsultation = new Consultation
                    {
                        ClinicalRecordId = record.id,
                        AppointmentId = followUpAppointment.id,
                        ParentConsultationId = consultation.id,
                        PracticePractitionerId = Guid.Parse(request.practicePractitionerId),
                        PatientId = request.patientId,
                        VisitDate = DateTime.UtcNow,
                        RequiresFollowUp = request.isfollowUpRequired,
                        ConsultationNotes = string.Empty,
                        reasonforvisit = request.FollowUpReason,
                        BillingType = consultation.BillingType,
                        SpecialtyTypes = consultation.SpecialtyTypes,
                        OrganisationId = appointmentconfimation.HouseholdId,
                        status = ConsultationStatus.InProgress,
                        createddate = DateTime.UtcNow,
                        createdby = request.practicePractitionerId
                    };
                    await _context.Consultations.AddAsync(followUpConsultation, cancellationToken);
                }
                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return Result<string>.Ok("Consultation + Invoice created successfully");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                return Result<string>.Fail(ex.Message);
            }
        }
        private async Task<Result<bool>> CreateSpecialtyConsultation(CreateConsultationCommand request, Guid consultationId, CancellationToken cancellationToken)
        {
            switch (request.SpecialtyType)
            {
                case SpecialtyType.Cardiology:
                    if (request.CardiologyData == null)
                        return Result<bool>.Fail("Cardiology data is required");
                    if (request.CardiologyData.ECGPerformed && string.IsNullOrWhiteSpace(request.CardiologyData.ECGFindings))
                        return Result<bool>.Fail("ECG findings required when ECG is performed");
                    var cardiology = new CardiologyConsultation
                    {
                        ConsultationId = consultationId,
                        ECGPerformed = request.CardiologyData.ECGPerformed,
                        ECGFindings = request.CardiologyData.ECGFindings,
                        EchoPerformed = request.CardiologyData.EchoPerformed,
                        EjectionFraction = request.CardiologyData.EjectionFraction,
                        SmokingRisk = request.CardiologyData.SmokingRisk,
                        HypertensionRisk = request.CardiologyData.HypertensionRisk,
                        createdby = request.practicePractitionerId
                    };
                    await _context.CardiologyConsultations.AddAsync(cardiology, cancellationToken);
                    break;

                case SpecialtyType.Dermatology:
                    if (request.DermatologyData == null)
                        return Result<bool>.Fail("Dermatology data is required");
                    if (string.IsNullOrWhiteSpace(request.DermatologyData.SkinCondition))
                        return Result<bool>.Fail("Skin condition is required");

                    var dermatology = new DermatologyConsultation
                    {
                        ConsultationId = consultationId,
                        SkinCondition = request.DermatologyData.SkinCondition,
                        Severity = request.DermatologyData.Severity,
                        AffectedAreas = request.DermatologyData.AffectedAreas,
                        ClinicalImages = request.DermatologyData.ClinicalImages?.Select(img => new ClinicalImage
                        {
                            ImageUrl = img.ImageUrl,
                            Description = img.Description,
                            CapturedAt = DateTime.UtcNow
                        }).ToList() ?? new List<ClinicalImage>(),
                        createdby = request.practicePractitionerId
                    };
                    await _context.DermatologyConsultations.AddAsync(dermatology, cancellationToken);
                    break;

                case SpecialtyType.Psychiatry:
                    if (request.PsychiatryData == null)
                        return Result<bool>.Fail("Psychiatry data is required");
                    if (request.PsychiatryData.SuicidalIdeation && string.IsNullOrWhiteSpace(request.PsychiatryData.SelfHarmRiskLevel))
                        return Result<bool>.Fail("Self-harm risk level required when suicidal ideation is present");

                    var psychiatry = new PsychiatryConsultation
                    {
                        ConsultationId = consultationId,
                        Mood = request.PsychiatryData.Mood,
                        Affect = request.PsychiatryData.Affect,
                        Speech = request.PsychiatryData.Speech,
                        SuicidalIdeation = request.PsychiatryData.SuicidalIdeation,
                        SelfHarmRiskLevel = request.PsychiatryData.SelfHarmRiskLevel,
                        TherapyNotes = request.PsychiatryData.TherapyNotes,
                        createdby = request.practicePractitionerId
                    };
                    await _context.PsychiatryConsultations.AddAsync(psychiatry, cancellationToken);
                    break;

                case SpecialtyType.Pediatrics:
                    if (request.PediatricsData == null)
                        return Result<bool>.Fail("Pediatrics data is required");

                    var pediatrics = new PediatricsConsultation
                    {
                        ConsultationId = consultationId,
                        WeightKg = request.PediatricsData.WeightKg,
                        HeightCm = request.PediatricsData.HeightCm,
                        VaccinationsUpToDate = request.PediatricsData.VaccinationsUpToDate,
                        DevelopmentalMilestones = request.PediatricsData.DevelopmentalMilestones,
                        createdby = request.practicePractitionerId
                    };
                    await _context.PediatricsConsultations.AddAsync(pediatrics, cancellationToken);
                    break;

                case SpecialtyType.Oncology:
                    if (request.OncologyData == null)
                        return Result<bool>.Fail("Oncology data is required");

                    var oncology = new OncologyConsultation
                    { 
                        ConsultationId = consultationId,
                        CancerType = request.OncologyData.CancerType,
                        CancerStage = request.OncologyData.CancerStage,
                        ChemotherapyCycle = request.OncologyData.ChemotherapyCycle,
                        TumorResponse = request.OncologyData.TumorResponse,
                        createdby = request.practicePractitionerId
                    };
                    await _context.OncologyConsultations.AddAsync(oncology, cancellationToken);
                    break;

                case SpecialtyType.Dentistry:
                    if (request.DentistryData == null)
                        return Result<bool>.Fail("Dentistry data is required");

                    var dentistry = new DentistryConsultation
                    {
                        ConsultationId = consultationId,
                        XRayCompleted = request.DentistryData.XRayCompleted,
                        GumCondition = request.DentistryData.GumCondition,
                        ToothCharts = request.DentistryData.ToothCharts?.Select(tooth => new ToothChart
                        {
                            ToothNumber = tooth.ToothNumber,
                            Condition = tooth.Condition,
                            Notes = tooth.Notes,
                            createdby = request.practicePractitionerId
                        }).ToList() ?? new List<ToothChart>(),
                        createdby = request.practicePractitionerId
                    };
                    await _context.DentistryConsultations.AddAsync(dentistry, cancellationToken);
                    break;

                default:
                    return Result<bool>.Fail($"Unknown specialty type: {request.SpecialtyType}");
            }

            await _context.SaveChangesAsync(cancellationToken);
            return Result<bool>.Ok(true);
        }
    }
}
