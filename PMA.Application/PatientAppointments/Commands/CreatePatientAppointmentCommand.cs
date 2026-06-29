using AutoMapper;
using Common;
using MediatR;
using PMA.Application.Interface.Mapping;
using PMA.Domain.Entities;
using PMA.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.PatientAppointments.Commands
{
    public class CreatePatientAppointmentCommand : IRequest<Result<string>> 
    {
        public string Appointmentreference { get; set; } = null!;
        public string Appointmenttype { get; set; } = null!;
        public string Idnumber { get; set; }
        public string PractitionerId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public DateTime startappointment { get; set; }
        public DateTime endappointment { get; set; }
        public AppointmentStatus Status { get; set; }
        public string userid { get; set; }
        
    }
}
