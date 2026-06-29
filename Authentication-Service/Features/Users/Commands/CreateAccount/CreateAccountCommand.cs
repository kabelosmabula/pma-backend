using API.Service.SharedModels;
using Common;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace PMA.Application.Features.Users.Command.CreateAccount
{
    public class CreateAccountCommand : CreateAccountShared, IRequest<Result<string>>
    {
        [Required]
        public string Password { get; set; } = string.Empty;


    }
}
