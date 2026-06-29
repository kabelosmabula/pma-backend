
using Common;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMA.Application.Dtos;
using PMA.Persistence;

namespace PMA.Application.Features.MedicalPractice.Queries.GetMedicalPracticeById
{
    public class GetMedicalPracticeByIdHandler : IRequestHandler<GetMedicalPracticeByIdQuery, Result<PracticeDto>>
    {

        private readonly PMADBContext _dbContext;

        public GetMedicalPracticeByIdHandler(PMADBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<PracticeDto>> Handle(GetMedicalPracticeByIdQuery request, CancellationToken cancellationToken)
        {
            var practice = await _dbContext.Practice.Where(x => x.id == request.PracticeId && x.isdeleted == false).FirstOrDefaultAsync() ?? throw new Exception("Practice not found");
            return Result<PracticeDto>.Ok(practice.Adapt<PracticeDto>());
        }
    }
}
