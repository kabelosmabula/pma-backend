using Common;
using MediatR;
using PMA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.PatientConsultation.Queries.GetConsultationById
{
    public class GetConsultationByIdQuery : IRequest<Result<Consultation>>
    {
        public string ConsultationId { get; set; }
    }
}
