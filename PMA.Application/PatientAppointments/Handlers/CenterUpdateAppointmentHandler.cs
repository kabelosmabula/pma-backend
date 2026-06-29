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
    public class CenterUpdateAppointmentHandler : IRequestHandler<CenterUpdateAppointment, Result<string>>
    {
        private readonly PMADBContext _context;

        public CenterUpdateAppointmentHandler(PMADBContext context)
        {
            _context = context;
        }
        public async Task<Result<string>> Handle(CenterUpdateAppointment request, CancellationToken cancellationToken)
        {
            try
            {
                var appointment = await _context.Appointments
                    .FirstOrDefaultAsync(x => (x.id == Guid.Parse(request.AppointmentId) )&&(x.PatientId == Guid.Parse(request.patientid))&& (x.createdby == request.userid) && !x.isdeleted, cancellationToken);

                if (appointment == null)
                    return Result<string>.Fail("Appointment not found");

                DateTime currentdate = DateTime.UtcNow;

                var updatedate = DateOnly.FromDateTime(currentdate);

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

                var doctor = await _context.PracticePractitioners
                    .FirstOrDefaultAsync(x => (x.id == Guid.Parse(request.PractitionerId))&&(!x.isdeleted),cancellationToken);
                if(doctor == null)
                    return Result<string>.Fail("Doctor does not exist in this Practice");

                appointment.Appointmentreference = request.Appointmentreference;
                appointment.Appointmenttype = request.Appointmenttype;
                appointment.AppointmentDate = appointmentDate;
                appointment.HouseholdId = appointment.HouseholdId;
                appointment.PractitionerId = doctor.id;
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
            throw new NotImplementedException();
        }
    }
}
