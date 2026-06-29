using Common;
using MediatR;

namespace API.Service.Features.MedicalPractice.Command.DeleteMedicalPractice
{
    public class DeleteMedicalPracticeCommand : IRequest<Result<string>>
    {
        public Guid PracticeId { get; set; }

        public Guid UserId { get; set; }
    }
}
