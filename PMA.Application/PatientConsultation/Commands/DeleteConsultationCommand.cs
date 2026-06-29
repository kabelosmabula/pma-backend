using Common;
using MediatR;
using PMA.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.PatientConsultation.Commands
{
    public class DeleteConsultationCommand : IRequest<Result<string>>
    {
        public string ConsultationId { get; set; }
        public string PracticePractitionerId { get; set; }
        public SpecialtyType SpecialtyTypes { get; set; }
    }
}
