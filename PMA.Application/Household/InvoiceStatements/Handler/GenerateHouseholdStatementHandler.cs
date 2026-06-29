using Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMA.Application.Household.InvoiceStatements.Command;
using PMA.Domain.Entities;
using PMA.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.Household.InvoiceStatements.Handler
{
    public class GenerateHouseholdStatementHandler : IRequestHandler<GenerateHouseholdStatementCommand, Result<string>>
    {
        private readonly PMADBContext _context;
        public GenerateHouseholdStatementHandler(PMADBContext context)
        {
            _context = context;
        }
        public async Task<Result<string>> Handle(GenerateHouseholdStatementCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var household = await _context.PatientHousehold
                    .FirstOrDefaultAsync(x => x.id == Guid.Parse(request.HouseholdId), cancellationToken);
                if (household == null)
                    return Result<string>.Fail("Household not found");
                var invoices = await _context.Invoices
                    .Include(i => i.Consultation)
                        .ThenInclude(c => c.ClinicalRecord)
                    .Where(i => i.Consultation.ClinicalRecord.Patient.HouseholdId == Guid.Parse(request.HouseholdId) &&
                        i.createddate >= request.StartDate &&
                        i.createddate <= request.EndDate &&
                        !i.isdeleted)
                    .ToListAsync(cancellationToken);
                if (!invoices.Any())
                    return Result<string>.Fail("No invoices found");
                var statement = new HouseholdInvoice
                {
                    HouseholdId = Guid.Parse(request.HouseholdId),
                    BillingPeriodStart = request.StartDate,
                    BillingPeriodEnd = request.EndDate,
                    Status = "Draft",
                    createddate = DateTime.UtcNow
                };
                decimal total = 0;
                foreach (var invoice in invoices)
                {
                    var item = new HouseholdInvoiceItem
                    {
                        InvoiceId = invoice.id,
                        HouseholdInvoice = statement,
                        createddate = DateTime.UtcNow
                    };
                    total += invoice.TotalAmount;
                    statement.Items.Add(item);
                }
                statement.TotalAmount = total;
                await _context.AddAsync(statement, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return Result<string>.Ok("Household statement generated successfully");
            }
            catch (Exception ex)
            {
                return Result<string>.Fail(ex.Message);
            }
        }
    }
}
