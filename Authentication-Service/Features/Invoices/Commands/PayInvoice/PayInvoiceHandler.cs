using Common;
using DocumentFormat.OpenXml.Bibliography;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMA.Domain.Entities;
using PMA.Persistence;

namespace API.Service.Features.Invoices.Commands.PayInvoice
{
    public class PayInvoiceHandler : IRequestHandler<PayInvoiceCommand, Result<string>>
    {

        private readonly PMADBContext _dbContext;

        public PayInvoiceHandler(PMADBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<string>> Handle(PayInvoiceCommand request, CancellationToken cancellationToken)
        {

            using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

            try
            {


                if (request.PaymentMethod != "Cash") throw new Exception("Payment option coming soon");
                if (request.Amount == 0) throw new Exception("Total amount cannot be zero");

                var invoice = await _dbContext.Invoices.Where(x => x.id == request.InvoiceId).FirstOrDefaultAsync() ?? throw new Exception("Invoice not found");
                
                if (invoice.OutStandingAmount == 0 && invoice.Status == "Paid") throw new Exception("The invoice is already paid up");

                if (request.Amount > invoice.OutStandingAmount ) throw new Exception($"Amount paid is more than the expected amount to be paid, The outstanding amount is R{invoice.OutStandingAmount}");
                
                invoice.OutStandingAmount -= request.Amount;
                if (invoice.OutStandingAmount == 0) invoice.Status = "Paid";

                var payment = new Payment
                {
                    id = Guid.NewGuid(),
                    Amount = request.Amount,
                    createddate = DateTime.UtcNow,
                    InvoiceId = request.InvoiceId,
                    createdby = request.UserId.ToString(),
                    Status = request.PaymentMethod == "Cash" ? "Paid" : "Pending",  
                    PaymentMethod = request.PaymentMethod
                };

                _dbContext.Update(invoice); 

                await _dbContext.AddAsync(payment); 
                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();

                return Result<string>.Ok($"Amount of R{request.Amount} was paid successfully , and the current outstanding amount is R{invoice.OutStandingAmount}");


            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                return Result<string>.Fail(ex.Message);
            }
        }
    }
}
