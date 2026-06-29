using Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMA.Persistence;

namespace API.Service.Features.MedicalPractice.Command.DeleteMedicalPractice
{
    public class DeleteMedicalPracticeHandler : IRequestHandler<DeleteMedicalPracticeCommand, Result<string>>
    {
        private readonly PMADBContext _dbContext;


        public DeleteMedicalPracticeHandler(PMADBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<string>> Handle(DeleteMedicalPracticeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var medicalPractice = await _dbContext.Practice.Where(x => x.id == request.PracticeId && x.isdeleted != true).FirstOrDefaultAsync() ?? throw new Exception("Practice not found");

                var user = await _dbContext.Users.Where(x => x.id == request.UserId).FirstOrDefaultAsync() ?? throw new Exception("User not found ");

                if (medicalPractice.createdby != user.id.ToString()) throw new Exception("Sorry you are not the owner of this practice! ");

                medicalPractice.deleteddate = DateTime.UtcNow;
                medicalPractice.deletedby = user.id.ToString();
                medicalPractice.isdeleted = true;

                _dbContext.Update(medicalPractice);
                await _dbContext.SaveChangesAsync();

                return Result<string>.Ok($"{medicalPractice.Name} was deleted successfully");
               
            }
            catch (Exception ex)
            {
                return Result<string>.Fail(ex.Message);
            }
        }
    }
}
