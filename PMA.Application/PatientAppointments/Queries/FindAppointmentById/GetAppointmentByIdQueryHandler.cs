using Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMA.Domain.Entities;
using PMA.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.PatientAppointments.Queries.FindAppointmentById
{
    public class GetAppointmentByIdQueryHandler : IRequestHandler<GetAppointmentByIdQuery, Result<Appointment>>
    {
        private readonly PMADBContext _context;

        public GetAppointmentByIdQueryHandler(PMADBContext context)
        {
            _context = context;
        }

        public async Task<Result<Appointment>> Handle(GetAppointmentByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var appointment = await _context.Appointments
                    .AsNoTracking()
                    .Include(x => x.Patient)
                    .Include(x => x.Practitioner)
                    .FirstOrDefaultAsync(x => x.id == Guid.Parse(request.AppointmentId) && !x.isdeleted, cancellationToken);

                if (appointment == null)
                    return Result<Appointment>.Fail("Appointment not found");

                return Result<Appointment>.Ok(appointment);
            }
            catch (Exception ex)
            {
                return Result<Appointment>.Fail(ex.Message);
            }
        }
    }
}
