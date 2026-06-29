using API.Service.Dtos;
using Common;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace API.Service.Features.Invoices.Queries.GetInvoiceById
{
    public class GetInvoiceByIdQuery : IRequest<Result<InvoiceDto>>
    {
        [Required]
        public Guid InvoiceId { get; set; }
    }
}
