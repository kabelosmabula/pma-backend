using Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using PMA.Persistence;

namespace Authentication.Service.Features.Users.Command.VerifyAccount
{
    public class VerifyAccountHandler : IRequestHandler<VerifyAccountCommand, Result<string>>
    {

        private readonly PMADBContext _dbContext;

        public VerifyAccountHandler(PMADBContext dbContext)
        {
            _dbContext = dbContext; 
        }

        public async Task<Result<string>> Handle(VerifyAccountCommand request, CancellationToken cancellationToken)
        {

            try
            {

                var user = await _dbContext.Users.Where(x => x.id == request.userId).FirstOrDefaultAsync() ?? throw new Exception("No profile found");

                if (user.Email != request.email) throw new Exception("Something went wrong , pleae try again");

                if (user.Isemailverified) throw new Exception("This email is already verified");

                user.Isemailverified = true; user.Accountstatus = "Active";

                await _dbContext.SaveChangesAsync();

                return Result<string>.Ok("Your account was verified successfully");

            }
            catch (Exception ex)
            {
                return Result<string>.Fail(ex.Message);
            }
        }


    }
}
