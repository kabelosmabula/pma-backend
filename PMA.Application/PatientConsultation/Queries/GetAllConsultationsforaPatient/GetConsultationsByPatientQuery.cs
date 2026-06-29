using Common;
using MediatR;
using PMA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.PatientConsultation.Queries.GetAllConsultationsforaPatient
{
    public class GetConsultationsByPatientQuery : IRequest<Result<List<Consultation>>>
    {
        public string PatientId { get; set; }
    }
}
