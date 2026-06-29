using Common;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMA.Persistence;

namespace API.Service.Features.Users.Command.UpdateAccountProfile
{
    public class UpdateAccountProfileHandler : IRequestHandler<UpdateAccountProfileCommand, Result<string>>
    {

        private readonly PMADBContext _dbContext;

        public UpdateAccountProfileHandler(PMADBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<string>> Handle(UpdateAccountProfileCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var user = await _dbContext.Users.Where(x => x.id == request.UserId).FirstOrDefaultAsync() ?? throw new Exception("User not found");

                if (user.createdby != request.UserId.ToString()) throw new Exception("You can only modify users created by you. ");

                request.Adapt(user);

                user.modifieddate = DateTime.UtcNow;

                user.modifiedby = request.UserId.ToString();

                _dbContext.Update(user);

                await _dbContext.SaveChangesAsync();

                return Result<string>.Ok("User updated successfully");

            }
            catch (Exception ex)
            {
                return Result<string>.Fail(ex.Message);
            }

        }
    }
}
