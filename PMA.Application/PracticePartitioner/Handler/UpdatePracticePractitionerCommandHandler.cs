using Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMA.Application.PracticePartitioner.Command;
using PMA.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.PracticePartitioner.Handler
{
    public class UpdatePracticePractitionerCommandHandler : IRequestHandler<UpdatePracticePractitionerCommand, Result<string>>
    {
        private readonly PMADBContext _context;

        public UpdatePracticePractitionerCommandHandler(PMADBContext context)
        {
            _context = context;
        }

        public async Task<Result<string>> Handle(UpdatePracticePractitionerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _context.PracticePractitioners
                    .FirstOrDefaultAsync(x => x.id == Guid.Parse(request.PracticePartitionerId) && !x.isdeleted, cancellationToken);

                if (entity == null)
                    return Result<string>.Fail("Practitioner assignment not found");

                if (!DateTime.TryParse(request.StartDate, out var startDate))
                    return Result<string>.Fail("Invalid StartDate");

                DateTime? endDate = null;

                if (!string.IsNullOrWhiteSpace(request.EndDate))
                {
                    if (!DateTime.TryParse(request.EndDate, out var parsedEnd))
                        return Result<string>.Fail("Invalid EndDate");

                    endDate = parsedEnd;
                }

                if (endDate.HasValue && endDate < startDate)
                    return Result<string>.Fail("EndDate cannot be before StartDate");

                entity.Specialty = request.Specialty;
                entity.PracticeEmail = request.PracticeEmail;
                entity.StartDate = startDate;
                entity.EndDate = endDate;
                entity.Isactive = request.Isactive;

                entity.modifieddate = DateTime.UtcNow;
                entity.modifiedby = request.userid;

                await _context.SaveChangesAsync(cancellationToken);

                return Result<string>.Ok("Practitioner updated successfully");
            }
            catch (Exception ex)
            {
                return Result<string>.Fail(ex.Message);
            }
        }
    }
}
