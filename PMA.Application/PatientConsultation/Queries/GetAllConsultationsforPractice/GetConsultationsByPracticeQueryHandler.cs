using Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMA.Domain.Entities;
using PMA.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.PatientConsultation.Queries.GetAllConsultationsforPractice
{
    public class GetConsultationsByPracticeQueryHandler
     : IRequestHandler<GetConsultationsByPracticeQuery, Result<List<Consultation>>>
    {
        private readonly PMADBContext _context;

        public GetConsultationsByPracticeQueryHandler(PMADBContext context)
        {
            _context = context;
        }

        public async Task<Result<List<Consultation>>> Handle(GetConsultationsByPracticeQuery request, CancellationToken cancellationToken)
        {
            var consultations = await _context.Consultations
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
                .Where(x => x.ClinicalRecord.PracticeId == Guid.Parse(request.PracticeId) && !x.isdeleted)
                .OrderByDescending(x => x.VisitDate)
                .ToListAsync(cancellationToken);

            return Result<List<Consultation>>.Ok(consultations);
        }
    }
}
