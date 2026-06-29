using Common;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMA.Persistence;
using System.Runtime.InteropServices;

namespace API.Service.Features.Users.Command.ManagePracticeAccessStatus
{
    public class ManagePracticeAccessStatusHandler : IRequestHandler<ManagePracticeAccessStatusCommand, Result<string>>
    {

        private readonly PMADBContext _dbContext;

        public ManagePracticeAccessStatusHandler(PMADBContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<Result<string>> Handle(ManagePracticeAccessStatusCommand request, CancellationToken cancellationToken)
        {

            try
            {

                var user = await _dbContext.Users.Where(x => x.id == request.UserId).FirstOrDefaultAsync() ?? throw new Exception("User not found");

                var practice = await _dbContext.Practice.Where(x => x.id == request.PracticeId).FirstOrDefaultAsync() ?? throw new Exception("Practice not found ");

                var userRole = await _dbContext.UserRoles.Where(x => x.PracticeId == practice.id && x.UserId == request.UserId).FirstOrDefaultAsync() ?? throw new Exception("This does not exist in your practice");

                userRole.modifieddate = DateTime.UtcNow;
                userRole.modifiedby = practice.id.ToString();
                userRole.Isactive = request.IsActive;

                _dbContext.Update(userRole);
                await _dbContext.SaveChangesAsync();

                var status = request.IsActive ? "Active " : "InActive";

                return Result<string>.Ok($"User practice access was updated successfully");
            }
            catch (Exception ex)
            {
                return Result<string>.Fail(ex.Message);
            }
        }
    }
}
