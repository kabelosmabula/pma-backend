using Common;
using MediatR;
using PMA.Application.Interface.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.PatientAppointments.Commands
{
    public class DeletePatientAppointmentCommand : IRequest<Result<string>>
    {
        public string AppointmentId { get; set; }
        public string userid { get; set; }
        
    }
}
