using Common;
using MediatR;
using PMA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.PatientAppointments.Queries.FindAppointmentById
{
    public class GetAppointmentByIdQuery : IRequest<Result<Appointment>>
    {
        public string AppointmentId { get; set; }
    }
}
