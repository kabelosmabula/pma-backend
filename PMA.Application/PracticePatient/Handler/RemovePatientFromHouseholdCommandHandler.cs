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
    public class RemovePatientFromHouseholdCommandHandler : IRequestHandler<RemovePatientFromHouseholdCommand, Result<string>>
    {
        private readonly PMADBContext _context;

        public RemovePatientFromHouseholdCommandHandler(PMADBContext context)
        {
            _context = context;
        }
        public async Task<Result<string>> Handle(RemovePatientFromHouseholdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var patient = await _context.Patients
                    .FirstOrDefaultAsync(x => x.id == Guid.Parse(request.PatientId), cancellationToken);
                if (patient == null)
                    return Result<string>.Fail("Patient not found");
                var currentHousehold = await _context.PatientHousehold
                    .Include(x => x.Patients)
                    .FirstOrDefaultAsync(x => x.id == patient.HouseholdId, cancellationToken);
                if (currentHousehold == null)
                    return Result<string>.Fail("Current household not found");
                if (currentHousehold.Patients.Count <= 1)
                    return Result<string>.Fail("Cannot remove last patient from household");
                if (request.NewHouseholdId != string.Empty)
                {
                    var newHousehold = await _context.PatientHousehold
                        .Include(x => x.Patients)
                        .FirstOrDefaultAsync(x => x.id == Guid.Parse(request.NewHouseholdId), cancellationToken);
                    if (newHousehold == null)
                        return Result<string>.Fail("Target household not found");
                    if (newHousehold.Patients.Count >= 5)
                        return Result<string>.Fail("Target household is full");
                    patient.HouseholdId = newHousehold.id;
                }        
                else if (request.CreateNewHousehold)
                {
                    var newHousehold = new PatientHousehold
                    {
                        HouseholdName = request.HouseholdName ?? "New Household",
                        createddate = DateTime.UtcNow
                    };
                    await _context.PatientHousehold.AddAsync(newHousehold, cancellationToken);
                    patient.HouseholdId = newHousehold.id;
                }
                else
                {
                    return Result<string>.Fail("No action specified");
                }
                await _context.SaveChangesAsync(cancellationToken);
                return Result<string>.Ok("Patient moved successfully");
            }
            catch (Exception ex)
            {
                return Result<string>.Fail(ex.Message);
            }
        }
    }
}
