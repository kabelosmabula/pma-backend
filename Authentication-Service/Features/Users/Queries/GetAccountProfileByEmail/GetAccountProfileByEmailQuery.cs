using API.Service.Dtos;
using Common;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace API.Service.Features.Users.Queries.GetAccountProfileByEmail
{
    public class GetAccountProfileByEmailQuery : IRequest<Result<UserDto>>
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
