using Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMA.Domain.Entities;
using PMA.Persistence;

namespace API.Service.Features.MedicalPractice.Command.LinkPracticePractioner
{
    public class LinkPracticePractionerHandler : IRequestHandler<LinkPracticePractionerCommand, Result<string>>
    {

        private readonly PMADBContext _dbContext;

        public LinkPracticePractionerHandler(PMADBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<string>> Handle(LinkPracticePractionerCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var user = await _dbContext.Users.Where(x => x.Email == request.Email.ToLowerInvariant()).FirstOrDefaultAsync() ?? throw new Exception("User not found");

                var practice = await _dbContext.Practice.Where(x => x.id == request.PracticeId && x.createdby == request.UserId.ToString() && x.isdeleted != true).FirstOrDefaultAsync() ?? throw new Exception("Practice not found ");

                var userRole = await _dbContext.UserRoles.Where(x => x.PracticeId == request.PracticeId && x.UserId == user.id).FirstOrDefaultAsync();

                if (userRole != null) throw new Exception($"{user.Displayname} already exist in the practice.");

                var role = await _dbContext.Roles.Where(x => x.id == request.RoleId).FirstOrDefaultAsync() ?? throw new Exception("Requested role not found");

                var accessRole = new UserRole { id = Guid.NewGuid(), UserId = user.id, RoleId = role.id,PracticeId = practice.id, createddate = DateTime.UtcNow, Isactive = true, createdby = request.UserId.ToString()};

                var userPractice = new UserPractice { id = Guid.NewGuid(), UserId = user.id, PracticeId = practice.id, createddate = DateTime.UtcNow, isActive = true, createdby = request.UserId.ToString() }; 

                await _dbContext.AddAsync(accessRole);
                await _dbContext.AddAsync(userPractice);

                await _dbContext.SaveChangesAsync();

                return Result<string>.Ok("User linked successfully");

            }
            catch (Exception ex)
            {
                return Result<string>.Fail(ex.Message);
            }

        }
    }
}
