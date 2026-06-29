using Common;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Authentication.Service.Features.Users.Command.VerifyAccount
{
    public class VerifyAccountCommand : IRequest<Result<string>>
    {
        
        [Required]
        [EmailAddress]
        public string email { get; set; } = string.Empty;

        [Required]
        public Guid userId { get; set; } 

    }
}
