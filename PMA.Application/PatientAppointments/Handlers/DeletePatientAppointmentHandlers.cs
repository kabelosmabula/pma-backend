using AutoMapper;
using Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMA.Application.PatientAppointments.Commands;
using PMA.Domain.Enums;
using PMA.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.PatientAppointments.Handlers
{
    public class DeletePatientAppointmentHandler
     : IRequestHandler<DeletePatientAppointmentCommand, Result<string>>
    {
        private readonly PMADBContext _context;

        public DeletePatientAppointmentHandler(PMADBContext context)
        {
            _context = context;
        }

        public async Task<Result<string>> Handle(DeletePatientAppointmentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var appointment = await _context.Appointments
                    .FirstOrDefaultAsync(x => x.id == Guid.Parse(request.AppointmentId), cancellationToken);
                if (appointment == null)
                    return Result<string>.Fail("Appointment not found");
                appointment.isdeleted = true;
                appointment.deleteddate = DateTime.UtcNow;
                appointment.Status = AppointmentStatus.Cancelled;
                appointment.deletedby = request.userid;
                await _context.SaveChangesAsync(cancellationToken);
                
                return Result<string>.Ok("Appointment cancelled");
            }
            catch (Exception ex)
            {
                return Result<string>.Fail(ex.Message);
            }
        }
    }
}
