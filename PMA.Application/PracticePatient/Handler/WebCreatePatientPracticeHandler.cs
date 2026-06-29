using Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMA.Application.PracticePatient.Command;
using PMA.Domain.Entities;
using PMA.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.PracticePatient.Handler
{
    public class WebCreatePatientPracticeHandler : IRequestHandler<WebCreatePatientPracticeCommand, Result<string>>
    {
        private readonly PMADBContext _context;

        public WebCreatePatientPracticeHandler(PMADBContext context)
        {
            _context = context;
        }

        public async Task<Result<string>> Handle(WebCreatePatientPracticeCommand request, CancellationToken cancellationToken)
        {
            using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                var exists = await _context.Patients
                    .FirstOrDefaultAsync(x =>
                        x.Identitynumber == request.idNumber &&
                        x.PracticeId == Guid.Parse(request.PracticeId) &&
                        !x.isdeleted,
                        cancellationToken);

                if (exists != null)
                    return Result<string>.Fail("Patient already exists");

                var practiceExists = await _context.Practice
                    .FirstOrDefaultAsync(x => x.id == Guid.Parse(request.PracticeId) && !x.isdeleted, cancellationToken);

                if (practiceExists == null)
                    return Result<string>.Fail("Invalid PracticeId. Practice does not exist.");

                Guid householdId;
     
                    var household = new PatientHousehold
                    {
                        PracticeId = practiceExists.id,
                        HouseholdName = $"{request.lastName} Family",
                        PrimaryContactName = request.firstName,
                        PrimaryContactSurName = request.lastName,
                        PrimaryContactPhone = request.phone,
                        Email = request.email,
                        Address = request.Addresses != null ? $"{request.Addresses.StreetAddress}, {request.Addresses.City}, {request.Addresses.Province}, {request.Addresses.PostalCode}" : null,
                        createddate = DateTime.UtcNow,
                        createdby = request.idNumber
                    };

                    await _context.PatientHousehold.AddAsync(household, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);
                    householdId = household.id;

                var patient = new Patient
                {
                    PracticeId = Guid.Parse(request.PracticeId),
                    HouseholdId = householdId,
                    FirstName = request.firstName,
                    LastName = request.lastName,
                    Displayname = $"{request.firstName} {request.lastName}",
                    Identitynumber = request.idNumber,
                    Email = request.email,
                    Phonenumber = request.phone,
                    Gender = request.gender,
                    Dateofbirth = DateOnly.TryParse(request.dateOfBirth, out var dob) ? dob : null,
                    createdby = request.idNumber,

                    Allergies = request.allergiesAndAlerts?.Allergies?
                    .Select(a => new Allergy
                    {
                        AllergyName = a,
                        createdby = request.email
                    }).ToList() ?? new List<Allergy>()
                };
                await _context.Patients.AddAsync(patient, cancellationToken);

                var clinical = new ClinicalRecord
                {
                    Patient = patient,
                    PracticeId = Guid.Parse(request.PracticeId),
                    createdby = request.idNumber
                };

                await _context.ClinicalRecords.AddAsync(clinical, cancellationToken);

                if (request.MedicalAids != null)
                {
                    var medicalAid = new MedicalAid
                    {
                        Patient = patient,
                        SchemeCode = request.MedicalAids.SchemeCode,
                        SchemeName = request.MedicalAids.Provider,
                        PlanCode = request.MedicalAids.PlanCode,
                        PlanName = request.MedicalAids.PlanName,
                        MembershipNumber = request.MedicalAids.MemberNumber,
                        DependentCode = "xxxx",
                        IsActive = true,
                        createdby = request.idNumber
                    };

                    await _context.MedicalAids.AddAsync(medicalAid, cancellationToken);
                    patient.MedicalAidId = medicalAid.id;

                    var households = await _context.PatientHousehold
                        .FirstOrDefaultAsync(h => h.id == householdId && !h.isdeleted, cancellationToken);
                    if (households != null)
                    {
                        households.MedicalAidId = medicalAid.id;
                        _context.PatientHousehold.Update(household);
                    }
                }

                if (request.emergencyContacts != null)
                {
                    var emergencyContact = new EmergencyContact
                    {

                        name = request.emergencyContacts.Name,
                        relationship = request.emergencyContacts.Relationship,
                        phone = request.emergencyContacts.Phone,
                        createdby = request.idNumber
                    };
                }
                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                return Result<string>.Ok("Patient created successfully");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                return Result<string>.Fail(ex.Message);
            }
        }
    }
}
