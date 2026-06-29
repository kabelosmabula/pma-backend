using Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMA.Domain.Entities;
using PMA.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.PatientConsultation.Queries.GetAllConsultationsforaPatient
{
    public class GetConsultationsByPatientQueryHandler : IRequestHandler<GetConsultationsByPatientQuery, Result<List<Consultation>>>
    {
        private readonly PMADBContext _context;

        public GetConsultationsByPatientQueryHandler(PMADBContext context)
        {
            _context = context;
        }

        public async Task<Result<List<Consultation>>> Handle(GetConsultationsByPatientQuery request, CancellationToken cancellationToken)
        {
            var consultations = await _context.Consultations
                .Include(x => x.Vital)
                .Include(x => x.Invoice)
                .Where(x => x.ClinicalRecord.PatientId == Guid.Parse(request.PatientId) && !x.isdeleted)
                .OrderByDescending(x => x.VisitDate)
                .ToListAsync(cancellationToken);

            return Result<List<Consultation>>.Ok(consultations);
        }
    }
}
