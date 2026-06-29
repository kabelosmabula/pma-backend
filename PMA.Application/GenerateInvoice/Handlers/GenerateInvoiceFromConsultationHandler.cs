using Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMA.Application.GenerateInvoice.Command;
using PMA.Domain.Entities;
using PMA.Domain.Enums;
using PMA.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.GenerateInvoice.Handlers
{
    public class GenerateInvoiceFromConsultationHandler: IRequestHandler<GenerateInvoiceFromConsultationCommand, Result<string>>
    {
        private readonly PMADBContext _context;

        public GenerateInvoiceFromConsultationHandler(PMADBContext context)
        {
            _context = context;
        }

        public async Task<Result<string>> Handle(GenerateInvoiceFromConsultationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var consultation = await _context.Consultations
                    .Include(x => x.Procedures)
                    .Include(x => x.ClinicalRecord)
                        .ThenInclude(cr => cr.Patient)
                    .FirstOrDefaultAsync(x => x.id == request.ConsultationId && !x.isdeleted, cancellationToken);

                if (consultation == null)
                    return Result<string>.Fail("Consultation not found");

                var existingInvoice = await _context.Invoices
                    .FirstOrDefaultAsync(x => x.ConsultationId == consultation.id, cancellationToken);

                if (existingInvoice != null)
                    return Result<string>.Fail("Invoice already exists for this consultation");

                Guid? medicalAidId = null;
                Guid patientId = Guid.Empty;

                if (consultation.BillingType == BillingType.Household)
                {
                    var patient = consultation.ClinicalRecord.Patient;

                    if (patient?.HouseholdId == null)
                        return Result<string>.Fail("Patient is not linked to a household");

                    var household = await _context.PatientHousehold
                        .FirstOrDefaultAsync(x => x.id == patient.HouseholdId, cancellationToken);

                    medicalAidId = household?.MedicalAidId;
                }else if (consultation.BillingType == BillingType.Cash)
                {
                    patientId = consultation.ClinicalRecord.PatientId;
                }

                var invoice = new Invoice
                {
                    ConsultationId = consultation.id,
                    PatientMedicalAidId = medicalAidId,
                    PatientId = patientId,
                    LineItems = new List<InvoiceLineItem>(),
                    createddate = DateTime.UtcNow,
                    createdby = "system"
                };

                decimal total = 0;

                var consultationFee = new InvoiceLineItem
                {
                    ReferenceCode = "CONSULT",
                    Description = "Doctor Consultation Fee",
                    Amount = 500, 
                    Invoice = invoice,
                    createddate = DateTime.UtcNow,
                    createdby = "system"
                };

                total += consultationFee.Amount;
                invoice.LineItems.Add(consultationFee);
                foreach (var procedure in consultation.Procedures)
                {
                    var item = new InvoiceLineItem
                    {
                        id = Guid.NewGuid(),
                        ReferenceCode = procedure.code,
                        Description = procedure.description,
                        Amount = procedure.tariffAmount,
                        Invoice = invoice,
                        createddate = DateTime.UtcNow,
                        createdby = "system"
                    };

                    total += item.Amount;
                    invoice.LineItems.Add(item);
                }

                invoice.TotalAmount = total;

                await _context.Invoices.AddAsync(invoice, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return Result<string>.Ok("Invoice generated successfully");
            }
            catch (Exception ex)
            {
                return Result<string>.Fail(ex.Message);
            }
        }
    }
}
