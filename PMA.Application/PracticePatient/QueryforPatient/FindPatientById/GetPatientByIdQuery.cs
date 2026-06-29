using Common;
using MediatR;
using PMA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.PracticePatient.QueryforPatient.FindPatientById
{
    public class GetPatientByIdQuery : IRequest<Result<Patient>>
    {
        public string PatientId { get; set; }
        public string PracticeId { get; set; }
    }
}
