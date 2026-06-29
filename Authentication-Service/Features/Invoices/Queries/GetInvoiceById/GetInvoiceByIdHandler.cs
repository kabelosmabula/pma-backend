using API.Service.Dtos;
using Common;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMA.Persistence;

namespace API.Service.Features.Invoices.Queries.GetInvoiceById
{
    public class GetInvoiceByIdHandler : IRequestHandler<GetInvoiceByIdQuery, Result<InvoiceDto>>
    {
        private readonly PMADBContext _dbContext;

        public GetInvoiceByIdHandler(PMADBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<InvoiceDto>> Handle(GetInvoiceByIdQuery request, CancellationToken cancellationToken)
        {
            var invoice = await _dbContext.Invoices.Where(x => x.id == request.InvoiceId).Include(x => x.LineItems).Include(x => x.Payments).FirstOrDefaultAsync() ?? throw new Exception("Invoice not found");  
            return Result<InvoiceDto>.Ok(invoice.Adapt<InvoiceDto>());
        }
    }
}
