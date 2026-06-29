
using Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMA.Application.Exceptions;
using PMA.Application.PracticePatient.Command;
using PMA.Domain.Entities;
using PMA.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.PracticePatient.Handler
{
    public class AddPatientToHouseHoldCommandHandler : IRequestHandler<AddPatientToHouseHoldCommand, Result<string>>
    {
        private readonly PMADBContext _context;
        public AddPatientToHouseHoldCommandHandler(PMADBContext context) 
		{ 
            _context= context;
		}
        public async Task<Result<string>> Handle(AddPatientToHouseHoldCommand request, CancellationToken cancellationToken)
        {
            using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var householdsummary = await _context.PatientHousehold
                    .Include(x => x.Patients)
                    .FirstOrDefaultAsync(x => x.id == Guid.Parse(request.HouseholdId) && !x.isdeleted, cancellationToken);

                if (householdsummary == null)
                    return Result<string>.Fail("Household does not exist");

                if (householdsummary.PracticeId != Guid.Parse(request.PracticeId))
                    return Result<string>.Fail("Household does not belong to this practice");

                if (householdsummary.Patients.Count >= 10)
                    return Result<string>.Fail("Household cannot exceed 10 members");

                var existingpatientsummary = await _context.Patients
                    .FirstOrDefaultAsync(x => x.Identitynumber == request.idNumber && x.PracticeId == Guid.Parse(request.PracticeId) && x.isdeleted == false, cancellationToken);

                if (existingpatientsummary != null)
                    return Result<string>.Fail("Patient already exists");

                DateOnly? dateOfBirth = null;
                if (!string.IsNullOrEmpty(request.dateOfBirth))
                {
                    if (DateOnly.TryParse(request.dateOfBirth, out var dob))
                        dateOfBirth = dob;
                }

                var patient = new Patient
                {
                    PracticeId = Guid.Parse(request.PracticeId),
                    HouseholdId = Guid.Parse(request.HouseholdId),
                    FirstName = request.firstName,
                    LastName = request.lastName,
                    Displayname = $"{request.firstName} {request.lastName}",
                    Identitynumber = request.idNumber,
                    Email = request.email,
                    MedicalAidId = householdsummary.MedicalAidId,
                    Phonenumber = request.phone,
                    Gender = request.gender,
                    Dateofbirth = dateOfBirth,
                    createdby = request.userid,
                    Allergies = request.allergiesAndAlerts?.Allergies?
                        .Select(a => new Allergy
                        {
                            AllergyName = a,
                            createdby = request.email
                        }).ToList() ?? new List<Allergy>()
                };
                await _context.Patients.AddAsync(patient, cancellationToken);

                var clinicalreco = new ClinicalRecord
                {
                    Patient = patient,
                    PracticeId = Guid.Parse(request.PracticeId),
                    createdby = request.userid
                };
                await _context.ClinicalRecords.AddAsync(clinicalreco, cancellationToken);

                if (request.emergencyContacts != null)
                {
                    var emergencyContact = new EmergencyContact
                    {
                        name = request.emergencyContacts.Name,
                        relationship = request.emergencyContacts.Relationship,
                        phone = request.emergencyContacts.Phone,
                        PatientId = patient.id, 
                        createdby = request.userid
                    };
                    await _context.EmergencyContacts.AddAsync(emergencyContact, cancellationToken);
                }
                if (request.Addresses != null)
                {
                    var address = new Address
                    {
                        streetAddress = request.Addresses.StreetAddress,
                        city = request.Addresses.City,
                        province = request.Addresses.Province,
                        postalCode = request.Addresses.PostalCode,
                        PatientId = patient.id, 
                        createdby = request.userid
                    };
                    await _context.Addresses.AddAsync(address, cancellationToken);
                }

                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                return Result<string>.Ok("Patient added to household successfully");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                return Result<string>.Fail(ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}
