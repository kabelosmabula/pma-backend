using Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMA.Domain.Entities;
using PMA.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.PatientConsultation.Queries.GetConsultationById
{
    public class GetConsultationByIdQueryHandler : IRequestHandler<GetConsultationByIdQuery, Result<Consultation>>
    {
        private readonly PMADBContext _context;

        public GetConsultationByIdQueryHandler(PMADBContext context)
        {
            _context = context;
        }

        public async Task<Result<Consultation>> Handle(GetConsultationByIdQuery request, CancellationToken cancellationToken)
        {
            var consultation = await _context.Consultations
                .Include(x => x.Vital)
                .Include(x => x.Diagnoses)
                .Include(x => x.Procedures)
                .Include(x => x.Documents)
                .Include(x => x.Prescriptions)
                .Include(x => x.Invoice)
                .Include(x => x.OncologyConsultation)
                .Include(x => x.CardiologyConsultation)
                .Include(x => x.PediatricsConsultation)
                .Include(x => x.PsychiatryConsultation)
                .Include(x => x.DermatologyConsultation)
                    .ThenInclude(x => x.ClinicalImages)
                .Include(x => x.DentistryConsultation)
                    .ThenInclude(x => x.ToothCharts)
                .AsSplitQuery()
                .FirstOrDefaultAsync(x => x.id == Guid.Parse(request.ConsultationId) && !x.isdeleted, cancellationToken);

            if (consultation == null)
                return Result<Consultation>.Fail("Consultation not found");

            return Result<Consultation>.Ok(consultation);
        }
    }
}
