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
    public class DeletePracticePractitionerCommandHandler : IRequestHandler<DeletePracticePractitionerCommand, Result<string>>
    {
        private readonly PMADBContext _context;

        public DeletePracticePractitionerCommandHandler(PMADBContext context)
        {
            _context = context;
        }

        public async Task<Result<string>> Handle(DeletePracticePractitionerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _context.PracticePractitioners
                    .FirstOrDefaultAsync(x => x.id == Guid.Parse(request.PracticePartitionerid) && !x.isdeleted, cancellationToken);

                if (entity == null)
                    return Result<string>.Fail("Practitioner assignment not found");

                entity.isdeleted = true;
                entity.deleteddate = DateTime.UtcNow;
                entity.deletedby = request.userid;
                entity.Isactive = false;
                entity.EndDate = DateTime.UtcNow;

                await _context.SaveChangesAsync(cancellationToken);

                return Result<string>.Ok("Practitioner removed from practice");
            }
            catch (Exception ex)
            {
                return Result<string>.Fail(ex.Message);
            }
        }
    }
}
