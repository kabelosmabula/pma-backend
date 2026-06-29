using Common;
using MediatR;
using PMA.Application.Interface.Mapping;
using PMA.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.PatientAppointments.Commands
{
    public class UpdatePatientAppointmentCommand : IRequest<Result<string>>
    {
        public string AppointmentId { get; set; }
        public string userid { get; set; }
        public string Appointmenttype { get; set; }
        public AppointmentStatus Status { get; set; }
        public DateTime AppointmentDate { get; set; }
        public DateTime startappointment { get; set; }
        public DateTime endappointment { get; set; }
    }
}
