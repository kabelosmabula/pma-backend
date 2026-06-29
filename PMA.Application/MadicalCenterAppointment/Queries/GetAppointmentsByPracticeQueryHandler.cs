using Common;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMA.Domain.Entities;
using PMA.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.MadicalCenterAppointment.Queries
{
    public class GetAppointmentsByPracticeQueryHandler
      : IRequestHandler<GetAppointmentsByPracticeQuery, Result<List<Appointment>>>
    {
        private readonly PMADBContext _context;

        public GetAppointmentsByPracticeQueryHandler(PMADBContext context)
        {
            _context = context;
        }

        public async Task<Result<List<Appointment>>> Handle(GetAppointmentsByPracticeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var appointments = await _context.Appointments
                    .AsNoTracking()
                    .Where(x => x.PracticeId == Guid.Parse(request.PracticeId) && !x.isdeleted)
                    .Include(x => x.Patient)
                    .Include(x => x.Practitioner)
                    .OrderBy(x => x.AppointmentDate)
                    .ThenBy(x => x.startappointment)
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
