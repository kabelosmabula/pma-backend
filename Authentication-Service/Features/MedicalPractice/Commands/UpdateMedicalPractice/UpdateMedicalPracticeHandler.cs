using Common;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMA.Persistence;

namespace PMA.Application.Features.MedicalPractice.Command.UpdateMedicalPractice
{
    public class UpdateMedicalPracticeHandler : IRequestHandler<UpdateMedicalPracticeCommand, Result<string>>
    {

        private readonly PMADBContext _dbContext;

        public UpdateMedicalPracticeHandler(PMADBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<string>> Handle(UpdateMedicalPracticeCommand request, CancellationToken cancellationToken)
        {
            
            try
            {

                var user = await _dbContext.Users.Where(x => x.id == request.UserId).FirstOrDefaultAsync() ?? throw new Exception("User not found");

                var practice = await _dbContext.Practice.Where(x => x.PracticeNumber == request.PracticeNumber).FirstOrDefaultAsync();

                if (practice is not null) throw new Exception("Practice already exist");

                var medicalPractice = await _dbContext.Practice.Where(x => x.id == request.id && x.isdeleted != true).FirstOrDefaultAsync() ?? throw new Exception($"Practice {request.id.ToString()} does not exist.");

                if (medicalPractice.createdby != user.id.ToString()) throw new Exception("Sorry you are not the owner of this practice! ");

                request.Adapt(medicalPractice);

                medicalPractice.modifieddate = DateTime.UtcNow;

                medicalPractice.modifiedby = request.UserId.ToString();

                _dbContext.Update(medicalPractice);

                await _dbContext.SaveChangesAsync();

                return Result<string>.Ok("Practice updated successfully");

            }
            catch (Exception ex)
            {
                return Result<string>.Fail(ex.Message);
            }
        }
    }
}
