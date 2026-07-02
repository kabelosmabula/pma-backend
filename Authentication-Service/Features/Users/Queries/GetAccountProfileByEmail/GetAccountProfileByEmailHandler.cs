using API.Service.Dtos;
using Common;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMA.Persistence;

namespace API.Service.Features.Users.Queries.GetAccountProfileByEmail
{
    public class GetAccountProfileByEmailHandler : IRequestHandler<GetAccountProfileByEmailQuery, Result<UserDto>>
    {
        
        private readonly PMADBContext _dbContext;
        public GetAccountProfileByEmailHandler(PMADBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Result<UserDto>> Handle(GetAccountProfileByEmailQuery request, CancellationToken cancellationToken)
        {
            var users = await _dbContext.Users.Where(x => x.Email == request.Email && x.isdeleted == false).FirstOrDefaultAsync() ?? throw new Exception("User not found");
            return Result<UserDto>.Ok(users.Adapt<UserDto>());
        }

    }
}
