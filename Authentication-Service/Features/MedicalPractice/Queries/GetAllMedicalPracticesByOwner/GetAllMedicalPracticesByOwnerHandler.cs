using Common;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMA.Application.Dtos;
using PMA.Persistence;
namespace PMA.Application.Features.GetAllMedicalPracticesByOwner.Queries.GetPracticesByOwner
{
    public class GetAllMedicalPracticesByOwnerHandler : IRequestHandler<GetAllMedicalPracticesByOwnerQuery, Result<IEnumerable<PracticeDto?>>>
    {

        private readonly PMADBContext _dbContext;

        public GetAllMedicalPracticesByOwnerHandler(PMADBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<IEnumerable<PracticeDto?>>> Handle(GetAllMedicalPracticesByOwnerQuery request, CancellationToken cancellationToken)
        {
            return Result<IEnumerable<PracticeDto?>>.Ok((await _dbContext.Practice.Where(x => x.createdby == request.UserId.ToString() && x.isdeleted == false).ToListAsync()).Adapt<List<PracticeDto>>());
        }
    }
}
