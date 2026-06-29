using Common;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace API.Service.Features.MedicalPractice.Command.LinkPracticePractioner
{
    public class LinkPracticePractionerCommand : IRequest<Result<string>>
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid PracticeId { get; set; }

        [Required]
        public Guid RoleId { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
