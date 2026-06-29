using AutoMapper;
using Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMA.Application.PatientAppointments.Commands;
using PMA.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.PatientAppointments.Handlers
{
    public class UpdatePatientAppointmentHandler : IRequestHandler<UpdatePatientAppointmentCommand, Result<string>>
    {
        private readonly PMADBContext _context;

        public UpdatePatientAppointmentHandler(PMADBContext context)
        {
            _context = context;
        }

        public async Task<Result<string>> Handle(UpdatePatientAppointmentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var appointment = await _context.Appointments
                    .FirstOrDefaultAsync(x => x.id == Guid.Parse(request.AppointmentId) && !x.isdeleted, cancellationToken);

                if (appointment == null)
                    return Result<string>.Fail("Appointment not found");

                DateTime currentdate = DateTime.UtcNow;

                var updatedate = DateOnly.FromDateTime(currentdate);
                if (updatedate == appointment.AppointmentDate || (appointment.AppointmentDate.Day - 1) == updatedate.Day) 
                    return Result<string>.Fail("Appointment cannot be rescheduled. is less than 24 hours of a Booked Appointment");

                if (request.startappointment >= request.endappointment)
                    return Result<string>.Fail("Invalid time");
                var appointmentDate = DateOnly.FromDateTime(request.AppointmentDate);
                var overlap = await _context.Appointments
                    .AnyAsync(x =>
                        x.PractitionerId == appointment.PractitionerId &&
                        x.id != appointment.id &&
                        x.AppointmentDate == appointmentDate &&
                        !x.isdeleted &&
                        (
                            request.startappointment < x.endappointment &&
                            request.endappointment > x.startappointment
                        ),
                        cancellationToken);
                if (overlap)
                    return Result<string>.Fail("Time slot already taken");
                appointment.Appointmenttype = request.Appointmenttype;
                appointment.AppointmentDate = appointmentDate;
                appointment.Status = request.Status;
                appointment.startappointment = request.startappointment;
                appointment.endappointment = request.endappointment;
                appointment.modifieddate = DateTime.UtcNow;
                appointment.modifiedby = request.userid;

                await _context.SaveChangesAsync(cancellationToken);
                return Result<string>.Ok("Appointment updated");
            }
            catch (Exception ex)
            {
                return Result<string>.Fail(ex.Message);
            }
        }
    }
}
