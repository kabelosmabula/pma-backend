using Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMA.Application.PracticePartitioner.Command;
using PMA.Domain.Entities;
using PMA.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.PracticePartitioner.Handler
{
    public class CreatePracticePractitionerCommandHandler: IRequestHandler<CreatePracticePractitionerCommand, Result<string>>
    {
        private readonly PMADBContext _context;

        public CreatePracticePractitionerCommandHandler(PMADBContext context)
        {
            _context = context;
        }

        public async Task<Result<string>> Handle(CreatePracticePractitionerCommand request, CancellationToken cancellationToken)
        {
            try
            {
 
                var practiceExists = await _context.Practice
                    .AnyAsync(x => x.id == Guid.Parse(request.PracticeId) && !x.isdeleted, cancellationToken);

                if (!practiceExists)
                    return Result<string>.Fail("Practice not found");

                var practitionerExists = await _context.Practitioners
                    .AnyAsync(x => x.id == Guid.Parse(request.PractitionerId) && !x.isdeleted, cancellationToken);

                if (!practitionerExists)
                    return Result<string>.Fail("Practitioner not found");

                var existing = await _context.PracticePractitioners
                    .FirstOrDefaultAsync(x =>
                        x.PracticeId == Guid.Parse(request.PracticeId) &&
                        x.PractitionerId == Guid.Parse(request.PractitionerId) &&
                        x. Isactive&&
                        !x.isdeleted,
                        cancellationToken);


                if (existing != null)
                    return Result<string>.Fail("Practitioner already active in this practice");

                var startDate = request.StartDate.Kind == DateTimeKind.Unspecified
                    ? DateTime.SpecifyKind(request.StartDate, DateTimeKind.Utc)
                    : request.StartDate.ToUniversalTime();

                DateTime? endDate = null;

                if (request.EndDate.HasValue)
                {
                    endDate = request.EndDate.Value.Kind == DateTimeKind.Unspecified
                        ? DateTime.SpecifyKind(request.EndDate.Value, DateTimeKind.Utc)
                        : request.EndDate.Value.ToUniversalTime();
                }

                var entity = new PracticePractitioner
                {

                    PracticeId = Guid.Parse(request.PracticeId),
                    PractitionerId = Guid.Parse(request.PractitionerId),
                    Specialty = request.Specialty,
                    PracticeEmail = request.PracticeEmail,
                    StartDate = startDate,
                    EndDate = endDate,
                    Isactive = request.IsActive,
                    createdby = request.PracticeEmail
                };

                await _context.PracticePractitioners.AddAsync(entity, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return Result<string>.Ok("Practitioner assigned successfully");
            }
            catch (Exception ex)
            {
                return Result<string>.Fail(ex.Message);
            }
        }
    }
}
