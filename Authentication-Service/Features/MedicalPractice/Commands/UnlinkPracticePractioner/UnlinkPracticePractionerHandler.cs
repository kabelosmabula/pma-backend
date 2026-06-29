using Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMA.Domain.Entities;
using PMA.Persistence;

namespace API.Service.Features.MedicalPractice.Commands.UnlinkPracticePractioner
{
    public class UnlinkPracticePractionerHandler : IRequestHandler<UnlinkPracticePractionerCommand, Result<string>>
    {
        private readonly PMADBContext _dbContext;

        public UnlinkPracticePractionerHandler(PMADBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<string>> Handle(UnlinkPracticePractionerCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var user = await _dbContext.Users.Where(x => x.Email == request.Email.ToLowerInvariant()).FirstOrDefaultAsync() ?? throw new Exception("User not found");
                if(user.id == request.UserId) throw new Exception("You can't remove yourself");

                var practice = await _dbContext.Practice.Where(x => x.id == request.PracticeId && x.isdeleted != true).FirstOrDefaultAsync() ?? throw new Exception("Practice not found ");
                var userRole = await _dbContext.UserRoles.Where(x => x.PracticeId == request.PracticeId && x.UserId == user.id).FirstOrDefaultAsync() ?? throw new Exception("User is not linked to the practice");

                userRole.isdeleted = true;
                userRole.deletedby = practice.createdby;
                userRole.deleteddate = DateTime.UtcNow;

                _dbContext.Update(userRole);
                await _dbContext.SaveChangesAsync();

                return Result<string>.Ok("User unlinked successfully");

            }
            catch (Exception ex)
            {
                return Result<string>.Fail(ex.Message);
            }
        }
    }
}
