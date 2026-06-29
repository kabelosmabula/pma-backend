using API.Service.Dtos;
using Common;
using MediatR;

namespace API.Service.Features.Users.Queries.GetAllRoles
{
    public class GetAllRolesQuery : IRequest<Result<IEnumerable<RolesDto>>>
    {

    }
}
