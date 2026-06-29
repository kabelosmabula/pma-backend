using Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMA.Domain.Entities;
using PMA.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.PracticePatient.QueryforPatient.FindPatientById
{
    public class GetPatientByIdQueryHandler : IRequestHandler<GetPatientByIdQuery, Result<Patient>>
    {
        private readonly PMADBContext _context;

        public GetPatientByIdQueryHandler(PMADBContext context)
        {
            _context = context;
        }

        public async Task<Result<Patient>> Handle(GetPatientByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var patient = await _context.Patients
                    .Include(p => p.Households)
                    .Include(p => p.MedicalAids)
                    .Include(p => p.Addresses)
                    .Include(p => p.EmergencyContacts)
                    .Include(p => p.Allergies)
                    .Include(p => p.ClinicalRecord)
                    .FirstOrDefaultAsync(p => p.id == Guid.Parse(request.PatientId)
                        && p.PracticeId == Guid.Parse(request.PracticeId)
                        && !p.isdeleted, cancellationToken);

                if (patient == null)
                    return Result<Patient>.Fail("Patient not found");

                return Result<Patient>.Ok(patient);
            }
            catch (Exception ex)
            {
                return Result<Patient>.Fail(ex.Message);
            }
        }
    }
}
