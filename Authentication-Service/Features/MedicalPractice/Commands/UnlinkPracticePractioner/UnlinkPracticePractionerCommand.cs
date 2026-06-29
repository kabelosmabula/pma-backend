using Common;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace API.Service.Features.MedicalPractice.Commands.UnlinkPracticePractioner
{
    public class UnlinkPracticePractionerCommand :IRequest<Result<string>>
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid PracticeId { get; set; }

        [Required]
        public string Email { get; set; } = string.Empty;
    }
}
