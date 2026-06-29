using Common;
using MediatR;
using PMA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.PatientConsultation.Queries.GetAllConsultationsforPractice
{
    public class GetConsultationsByPracticeQuery : IRequest<Result<List<Consultation>>>
    {
        public string PracticeId { get; set; }
    }
}
