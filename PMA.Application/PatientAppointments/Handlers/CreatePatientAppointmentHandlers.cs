using AutoMapper;
using Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMA.Application.PatientAppointments.Commands;
using PMA.Application.PracticePatient.Command;
using PMA.Domain.Entities;
using PMA.Domain.Enums;
using PMA.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.PatientAppointments.Handlers
{
    public class CreatePatientAppointmentHandlers : IRequestHandler<CreatePatientAppointmentCommand, Result<string>>
    {
        private readonly PMADBContext _context;
      
        public CreatePatientAppointmentHandlers(PMADBContext context)
        {
            _context = context;
        }

        public async Task<Result<string>> Handle(CreatePatientAppointmentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var patient = await _context.Patients
                    .FirstOrDefaultAsync(x =>x.Identitynumber == request.Idnumber &&  !x.isdeleted, cancellationToken);
                if (patient == null)
                    return Result<string>.Fail("Patient not found");

                var practitioner = await _context.PracticePractitioners
                    .FirstOrDefaultAsync(x => x.id == Guid.Parse(request.PractitionerId) && !x.isdeleted, cancellationToken);
                if (practitioner == null)
                    return Result<string>.Fail("Practitioner not found");
                
                if (request.startappointment >= request.endappointment)
                    return Result<string>.Fail("Invalid appointment time");

                var appointmentDate = DateOnly.FromDateTime(request.AppointmentDate);
                var overlap = await _context.Appointments
                    .AnyAsync(x =>
                        x.PractitionerId == Guid.Parse(request.PractitionerId) &&
                        x.AppointmentDate == appointmentDate &&
                        !x.isdeleted &&
                        (
                            request.startappointment < x.endappointment &&
                            request.endappointment > x.startappointment
                        ),
                        cancellationToken);
                if (overlap)
                    return Result<string>.Fail($"{practitioner.PracticeEmail} already has an appointment in this time slot");
          
                var appointment = new Appointment
                {
                    AppointmentDate = appointmentDate,
                    PatientId = patient.id,
                    PracticeId = patient.PracticeId,
                    HouseholdId = patient.HouseholdId,
                    Priority = "emergancy",
                    Appointmentreference = request.Appointmentreference,
                    Appointmenttype = request.Appointmenttype,
                    PractitionerId = practitioner.id,
                    startappointment = request.startappointment,
                    endappointment = request.endappointment,
                    Status  = request.Status,
                    createddate = DateTime.UtcNow,
                    createdby = request.userid
                };
                
                await _context.Appointments.AddAsync(appointment, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return Result<string>.Ok("Appointment booked successfully");
            }
            catch (Exception ex)
            {
                return Result<string>.Fail(ex.Message);
            }
        }
    }
}
