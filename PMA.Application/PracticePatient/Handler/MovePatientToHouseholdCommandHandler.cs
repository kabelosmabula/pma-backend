using Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMA.Application.PracticePatient.Command;
using PMA.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.PracticePatient.Handler
{
    public class MovePatientToHouseholdCommandHandler : IRequestHandler<MovePatientToHouseholdCommand, Result<string>>
    {
        private readonly PMADBContext _context;

        public MovePatientToHouseholdCommandHandler(PMADBContext context)
        {
            _context = context;
        }

        public async Task<Result<string>> Handle(MovePatientToHouseholdCommand  request, CancellationToken cancellationToken)
        {
            try
            {
                var patient = await _context.Patients
                    .FirstOrDefaultAsync(x => x.id == Guid.Parse(request.PatientId), cancellationToken);

                var household = await _context.PatientHousehold
                    .Include(x => x.Patients)
                    .FirstOrDefaultAsync(x => x.id == Guid.Parse(request.TargetHouseholdId), cancellationToken);

                if (patient == null || household == null)
                    return Result<string>.Fail("Invalid data");

                if (household.Patients.Count >= 5)
                    return Result<string>.Fail("Household full");

                    patient.HouseholdId = household.id;      

                await _context.SaveChangesAsync(cancellationToken);

                return Result<string>.Ok("Patient moved");
            }
            catch (Exception ex)
            {
                return Result<string>.Fail(ex.Message);
            }
        }
    }
}
