using API.Service.Dtos;
using Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMA.Persistence;

namespace API.Service.Features.MedicalPractice.Queries.GetPracticePractionersByPractice
{
    public class GetPracticePractionersByPracticeHandler : IRequestHandler<GetPracticePractionersByPracticeCommand, Result<IEnumerable<UserPracticeDto>>>
    {

        private readonly PMADBContext _dbContext;

        public GetPracticePractionersByPracticeHandler(PMADBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<IEnumerable<UserPracticeDto>>> Handle(GetPracticePractionersByPracticeCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var practioners = await _dbContext.UserRoles
                                        .Where(x => x.PracticeId == request.PracticeId && x.isdeleted != true)
                                        .Include(x => x.User)
                                        .Select(x => new UserPracticeDto
                                        {
                                             Firstname = x.User.Firstname,
                                             Lastname = x.User.Lastname,
                                             Displayname = x.User.Displayname,
                                             Email = x.User.Email,
                                             Isactive = x.Isactive,
                                             Phonenumber = x.User.Phonenumber,
                                        }).ToListAsync();

                return Result<IEnumerable<UserPracticeDto>>.Ok(practioners);

                 
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<UserPracticeDto>>.Fail(ex.Message);
            }


        }
    }
}
