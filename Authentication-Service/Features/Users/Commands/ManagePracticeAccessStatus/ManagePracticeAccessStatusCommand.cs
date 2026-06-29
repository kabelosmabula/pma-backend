using Common;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace API.Service.Features.Users.Command.ManagePracticeAccessStatus
{
    public class ManagePracticeAccessStatusCommand : IRequest<Result<string>>
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid PracticeId { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}
