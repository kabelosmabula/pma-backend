using API.Service.Dtos;
using Common;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMA.Persistence;

namespace API.Service.Features.Users.Queries.GetAllRoles
{
    public class GetAllRolesHandler : IRequestHandler<GetAllRolesQuery, Result<IEnumerable<RolesDto>>>
    {
        private readonly PMADBContext _dbContext;

        public GetAllRolesHandler(PMADBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<IEnumerable<RolesDto>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            return Result<IEnumerable<RolesDto>>.Ok((await _dbContext.Roles.ToListAsync()).Adapt<List<RolesDto>>());
        }
    }
}
