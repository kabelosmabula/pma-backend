using API.Service.Dtos;
using Common;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMA.Persistence;

namespace API.Service.Features.Invoices.Queries.GetInvoicesByPractice
{
    public class GetInvoicesByPracticeHandler : IRequestHandler<GetInvoicesByPracticeQuery, Result<List<InvoiceOnlyDto>>>
    {

        private readonly PMADBContext _dbContext;

        public GetInvoicesByPracticeHandler(PMADBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<List<InvoiceOnlyDto>>> Handle(GetInvoicesByPracticeQuery request, CancellationToken cancellationToken)
        {
            var invoice = await _dbContext.Invoices.Where(x => x.Consultation.PracticePractitioner.PracticeId == request.PraticeId).ToListAsync() ?? throw new Exception("Invoice not found");
            return Result<List<InvoiceOnlyDto>>.Ok(invoice.Adapt<List<InvoiceOnlyDto>>());
        }


    }
}
