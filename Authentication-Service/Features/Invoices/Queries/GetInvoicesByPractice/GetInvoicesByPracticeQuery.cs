using API.Service.Dtos;
using Common;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace API.Service.Features.Invoices.Queries.GetInvoicesByPractice
{
    public class GetInvoicesByPracticeQuery : IRequest<Result<List<InvoiceOnlyDto>>>
    {
        [Required]
        public Guid PraticeId { get; set; }
    }
}
