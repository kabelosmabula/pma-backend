using Common;
using MediatR;
using PMA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.PatientAppointments.Queries.FindAllAppointmentForPatient
{
    public class GetAppointmentsByPatientQuery : IRequest<Result<List<Appointment>>>
    {
        public string PatientId { get; set; }
    }
}
