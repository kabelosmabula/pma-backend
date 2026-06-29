using Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.GenerateInvoice.Command
{
    public class GenerateInvoiceFromConsultationCommand : IRequest<Result<string>>
    {
        public Guid ConsultationId { get; set; }
    }
}