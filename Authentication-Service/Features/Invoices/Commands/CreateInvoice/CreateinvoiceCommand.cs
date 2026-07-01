using API.Service.Models;
using API.Service.SharedModels;
using Common;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace API.Service.Features.Invoices.CreateInvoice.Commands.CreateInvoice
{
    public class CreateInvoiceCommand : InvoiceShared , IRequest<Result<string>>
    {
        public Guid UserId { get; set; }

    }
}
