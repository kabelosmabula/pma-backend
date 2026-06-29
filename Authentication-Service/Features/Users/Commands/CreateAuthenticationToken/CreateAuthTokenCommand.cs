using Common;
using MediatR;
using Models;
using System.ComponentModel.DataAnnotations;

namespace Authentication.Service.Features.Users.Command.CreateAuthenticationToken
{
    public class CreateAuthTokenCommand : IRequest<Result<AuthResponse>>
    {

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Enter a valid email address")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;

    }
}
