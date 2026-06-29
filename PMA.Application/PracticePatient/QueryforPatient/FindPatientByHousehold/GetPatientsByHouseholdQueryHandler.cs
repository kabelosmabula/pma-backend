using Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMA.Domain.Entities;
using PMA.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.PracticePatient.QueryforPatient.FindPatientByHousehold
{
    public class GetPatientsByHouseholdQueryHandler : IRequestHandler<GetPatientsByHouseholdQuery, Result<List<Patient>>>
    {
        private readonly PMADBContext _context;

        public GetPatientsByHouseholdQueryHandler(PMADBContext context)
        {
            _context = context;
        }
        public async Task<Result<List<Patient>>> Handle(GetPatientsByHouseholdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var patients = await _context.Patients
                    .Include(p => p.Allergies)
                    .Include(p => p.Addresses)
                    .Include(p => p.EmergencyContacts)
                    .Where(p => p.HouseholdId == Guid.Parse(request.HouseholdId)
                        && p.PracticeId == Guid.Parse(request.PracticeId)
                        && !p.isdeleted)
                    .OrderBy(p => p.FirstName)
                    .ToListAsync(cancellationToken);

                return Result<List<Patient>>.Ok(patients);
            }
            catch (Exception ex)
            {
                return Result<List<Patient>>.Fail(ex.Message);
            }
        }
    }
}
