using API.Service.SharedModels;
using Common;
using MediatR;

namespace API.Service.Features.Invoices.Commands.PayInvoice
{
    public class PayInvoiceCommand :PayInvoiceShared , IRequest<Result<string>>
    {
        public Guid UserId { get; set; }
    }
}
