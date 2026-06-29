using Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMA.Application.PatientConsultation.Commands;
using PMA.Domain.Entities;
using PMA.Domain.Enums;
using PMA.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.PatientConsultation.Handlers
{
    public class DeleteConsultationCommandHandler : IRequestHandler<DeleteConsultationCommand, Result<string>>
    {
        private readonly PMADBContext _context;
        public DeleteConsultationCommandHandler(PMADBContext context)
        {
            _context = context;
        }
        public async Task<Result<string>> Handle(DeleteConsultationCommand request,CancellationToken cancellationToken)
        {
            using var transaction =await _context.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                var consultation = await _context.Consultations
                    .Include(x => x.CardiologyConsultation)
                    .Include(x => x.DermatologyConsultation)
                        .ThenInclude(x => x.ClinicalImages)
                    .Include(x => x.PsychiatryConsultation)
                    .Include(x => x.PediatricsConsultation)
                    .Include(x => x.OncologyConsultation)
                    .Include(x => x.DentistryConsultation)
                        .ThenInclude(x => x.ToothCharts)
                    .FirstOrDefaultAsync(
                        x => x.id == Guid.Parse(request.ConsultationId) && !x.isdeleted,
                        cancellationToken);

                if (consultation == null)
                    return Result<string>.Fail("Consultation not found");

                SoftDeleteEntity( consultation,request.PracticePractitionerId.ToString());

                var vitals = await _context.Vitals
                    .Where(x => x.consultationId == consultation.id && !x.isdeleted)
                    .ToListAsync(cancellationToken);

                foreach (var vital in vitals)
                {
                    SoftDeleteEntity(vital, request.PracticePractitionerId.ToString());
                }

                var diagnoses = await _context.Diagnoses
                    .Where(x => x.ConsultationId == consultation.id && !x.isdeleted)
                    .ToListAsync(cancellationToken);

                foreach (var diagnosis in diagnoses)
                {
                    SoftDeleteEntity(diagnosis, request.PracticePractitionerId.ToString());
                }

                var procedures = await _context.Procedures
                    .Where(x => x.ConsultationId == consultation.id && !x.isdeleted)
                    .ToListAsync(cancellationToken);

                foreach (var procedure in procedures)
                {
                    SoftDeleteEntity(procedure, request.PracticePractitionerId.ToString());
                }

                var prescriptions = await _context.Prescriptions
                    .Where(x => x.consultationId == consultation.id && !x.isdeleted)
                    .ToListAsync(cancellationToken);

                foreach (var prescription in prescriptions)
                {
                    SoftDeleteEntity(prescription, request.PracticePractitionerId.ToString());
                }

                var documents = await _context.ClinicalDocuments
                    .Where(x => x.ConsultationId == consultation.id && !x.isdeleted)
                    .ToListAsync(cancellationToken);

                foreach (var doc in documents)
                {
                    SoftDeleteEntity(doc, request.PracticePractitionerId.ToString());
                }

                if (consultation.CardiologyConsultation != null)
                {
                    SoftDeleteEntity(
                        consultation.CardiologyConsultation,
                        request.PracticePractitionerId.ToString());
                }

                if (consultation.DermatologyConsultation != null)
                {
                    SoftDeleteEntity(
                        consultation.DermatologyConsultation,
                        request.PracticePractitionerId.ToString());

                    foreach (var image in consultation.DermatologyConsultation.ClinicalImages)
                    {
                        SoftDeleteEntity(
                            image,
                            request.PracticePractitionerId.ToString());
                    }
                }

                if (consultation.PsychiatryConsultation != null)
                {
                    SoftDeleteEntity(
                        consultation.PsychiatryConsultation,
                        request.PracticePractitionerId.ToString());
                }

                if (consultation.PediatricsConsultation != null)
                {
                    SoftDeleteEntity(
                        consultation.PediatricsConsultation,
                        request.PracticePractitionerId.ToString());
                }

                if (consultation.OncologyConsultation != null)
                {
                    SoftDeleteEntity(
                        consultation.OncologyConsultation,
                        request.PracticePractitionerId.ToString());
                }

                if (consultation.DentistryConsultation != null)
                {
                    SoftDeleteEntity(
                        consultation.DentistryConsultation,
                        request.PracticePractitionerId.ToString());

                    foreach (var tooth in consultation.DentistryConsultation.ToothCharts)
                    {
                        SoftDeleteEntity(
                            tooth,
                            request.PracticePractitionerId.ToString());
                    }
                }

                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                return Result<string>.Ok("Consultation deleted successfully");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);

                return Result<string>.Fail(
                    $"Delete failed: {ex.Message}");
            }
        }
        private void SoftDeleteEntity(BaseEntity entity, string deletedBy)
        {
            entity.isdeleted = true;
            entity.deleteddate = DateTime.UtcNow;
            entity.deletedby = deletedBy;
        }
    }
}
