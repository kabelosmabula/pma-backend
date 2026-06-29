using Common;
using MediatR;
using PMA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.MadicalCenterAppointment.Queries
{
    public class GetAppointmentsByPracticeQuery : IRequest<Result<List<Appointment>>>
    {
        public string PracticeId { get; set; }
    }
}
