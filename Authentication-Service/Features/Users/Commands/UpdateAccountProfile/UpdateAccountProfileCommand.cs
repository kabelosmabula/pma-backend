using API.Service.SharedModels;
using Common;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace API.Service.Features.Users.Command.UpdateAccountProfile
{
    public class UpdateAccountProfileCommand : CreateAccountShared , IRequest<Result<string>>
    {
        [Required]
        public Guid UserId { get; set; }

        public string? Phonenumber { get; set; }
    }
}
