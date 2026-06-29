using Authentication.Service.Helpers;
using Common;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMA.Domain.Entities;
using PMA.Persistence;

namespace API.Service.Features.Invoices.CreateInvoice.Commands.CreateInvoice
{
    public class CreateInvoiceHandler : IRequestHandler<CreateInvoiceCommand, Result<string>>
    {

        private readonly PMADBContext _dbContext;
        private readonly Helper _helper;

        public CreateInvoiceHandler(PMADBContext dbContext , Helper helper)
        {
            _dbContext = dbContext;
            _helper = helper;
        }

        public async Task<Result<string>> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {

            using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

            try
            {

                if (request.TotalAmount == 0) throw new Exception("Total amount cannot be zero");
                if (request.LineItems.Sum(x => x.Amount) != request.TotalAmount) throw new Exception("Total amount does not match the sum of line items");

                var currentInvoice = await _dbContext.Invoices.Where(x => x.ConsultationId == request.ConsultationId).FirstOrDefaultAsync();

                if (currentInvoice != null) throw new Exception("An invoice for this consultation already exists");

                var consultation = await _dbContext.Consultations.Where(x => x.id == request.ConsultationId).FirstOrDefaultAsync() ?? throw new Exception("Consultation not found");
                var patient = await _dbContext.Patients.Where(x => x.id == request.PatientId).FirstOrDefaultAsync() ?? throw new Exception("Patient not found");

                var user = await _dbContext.Users.Where(x => x.id == request.UserId).FirstOrDefaultAsync() ?? throw new Exception("User not found");
               
                var invoiceId = Guid.NewGuid();

                var invoice = new Invoice
                {
                    id = invoiceId,
                    PatientId = request.PatientId,
                    createddate = DateTime.UtcNow,
                    createdby = user.id.ToString(),
                    ConsultationId = consultation.id,
                    PatientMedicalAidId = patient.MedicalAidId,
                    HouseholdId = patient.HouseholdId,
                    TotalAmount = request.TotalAmount,
                    Status = "Pending",
                    OutStandingAmount = request.TotalAmount,
                };

                var lineItems = request.LineItems.Select(item => new InvoiceLineItem
                {
                    id = Guid.NewGuid(),
                    InvoiceId = invoice.id,
                    Description = item.Description,
                    ReferenceCode = $"ITEM-{_helper.GenerateFiveDigitNumber().ToString()}",
                    Amount = item.Amount,
                    createdby = user.id.ToString(),
                    createddate = DateTime.UtcNow,

                }).ToList();

                await _dbContext.Invoices.AddAsync(invoice);
                await _dbContext.InvoiceLineItems.AddRangeAsync(lineItems); 

                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();

                return Result<string>.Ok("Invoice created successfully");

            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                return Result<string>.Fail(ex.Message);
            }
        }
    }
}
