using Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMA.Domain.Entities;
using PMA.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.PatientAppointments.Queries.FindAllAppointmentForPatient
{
    public class GetAppointmentsByPatientQueryHandler : IRequestHandler<GetAppointmentsByPatientQuery, Result<List<Appointment>>>
    {
        private readonly PMADBContext _context;

        public GetAppointmentsByPatientQueryHandler(PMADBContext context)
        {
            _context = context;
        }

        public async Task<Result<List<Appointment>>> Handle(GetAppointmentsByPatientQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var appointments = await _context.Appointments
                    .AsNoTracking()
                    .Where(x => x.PatientId == Guid.Parse(request.PatientId) && !x.isdeleted)
                    .Include(x => x.Practitioner)
                    .OrderByDescending(x => x.startappointment)
                    .ToListAsync(cancellationToken);

                return Result<List<Appointment>>.Ok(appointments);
            }
            catch (Exception ex)
            {
                return Result<List<Appointment>>.Fail(ex.Message);
            }
        }
    }
}
