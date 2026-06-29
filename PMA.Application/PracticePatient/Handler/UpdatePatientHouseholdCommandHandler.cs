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
    public class UpdatePatientHouseholdCommandHandler : IRequestHandler<UpdatePatientHouseholdCommand, Result<string>>
    {
        private readonly PMADBContext _context;

        public UpdatePatientHouseholdCommandHandler(PMADBContext context)
        {
            _context = context;
        }

        public async Task<Result<string>> Handle(UpdatePatientHouseholdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var household = await _context.PatientHousehold
                    .Include(x => x.Patients)
                    .FirstOrDefaultAsync(x => x.id == Guid.Parse(request.HouseholdId), cancellationToken);
                if (household == null)
                    return Result<string>.Fail("Household not found");
                household.HouseholdName = request.HouseholdName;
                household.PrimaryContactName = request.PrimaryContactName;
                household.PrimaryContactSurName = request.PrimaryContactSurName;
                household.PrimaryContactPhone = request.PrimaryContactPhone;
                household.Email = request.Email;
                household.Address = request.Address;
                household.modifieddate = DateTime.UtcNow;
                foreach (var dto in request.Patients)
                {
                    var patient = household.Patients
                        .FirstOrDefault(x => x.id == Guid.Parse(dto.PatientId) && !x.isdeleted);
                    if (patient == null)
                        continue;
                    patient.FirstName = dto.FirstName;
                    patient.LastName = dto.LastName;
                    patient.Displayname = dto.Displayname;
                    patient.Identitynumber = dto.Identitynumber;
                    patient.Email = dto.Email;
                    patient.Phonenumber = dto.Phonenumber;
                    patient.Dateofbirth = dto.Dateofbirth;
                    patient.modifieddate = DateTime.UtcNow;
                }
                await _context.SaveChangesAsync(cancellationToken);
                return Result<string>.Ok("Household and patients updated");
            }
            catch (Exception ex)
            {
                return Result<string>.Fail(ex.Message);
            }
        }
    }
}
