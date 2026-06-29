using Common;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMA.Domain.Entities;
using PMA.Persistence;

namespace PMA.Application.Features.MedicalPractice.Command.AddMedicalPractice
{
    public class AddMedicalPracticeHandler : IRequestHandler<AddMedicalPracticeCommand, Result<string>>
    {

        private readonly PMADBContext _dbContext;

        public AddMedicalPracticeHandler(PMADBContext dbContext)
        {
             _dbContext = dbContext;
        }

        public async Task<Result<string>> Handle(AddMedicalPracticeCommand request, CancellationToken cancellationToken)
        {

            try
            {

                var user = await _dbContext.Users.Where(x => x.id == request.UserId).FirstOrDefaultAsync() ?? throw new Exception("User not found ");
                var practice = await  _dbContext.Practice.Where(x => x.PracticeNumber == request.PracticeNumber).FirstOrDefaultAsync();

                if (practice is not null) throw new Exception("Practice already exist");

                var medicalPractice = request.Adapt<Practice>();

                medicalPractice.createdby = request.UserId.ToString();

                medicalPractice.id = Guid.NewGuid();

                var role = await _dbContext.Roles.Where(x => x.Name == "Practice Admin").FirstOrDefaultAsync() ?? throw new Exception("Role not found");

                medicalPractice.createddate = DateTime.UtcNow;

                var accessRole = new UserRole { PracticeId = medicalPractice.id, id = Guid.NewGuid(), UserId = user.id, RoleId = role.id, createddate = DateTime.UtcNow, Isactive = true, createdby = user.id.ToString() };
                var userPractice = new UserPractice { id = Guid.NewGuid(), UserId = user.id, PracticeId = medicalPractice.id, createddate = DateTime.UtcNow, isActive = true, createdby = request.UserId.ToString() };

                await _dbContext.AddAsync(medicalPractice);
                await _dbContext.AddAsync(accessRole);
                await _dbContext.AddAsync(userPractice);

                await _dbContext.SaveChangesAsync();

                return Result<string>.Ok($"{medicalPractice.Name} ,practice was created successfully");

            }
            catch(Exception ex)
            {
                return Result<string>.Fail(ex.Message);
            }
            
        }
    }
}
