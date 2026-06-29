using API.Service.Dtos;
using Common;
using MediatR;

namespace API.Service.Features.MedicalPractice.Queries.GetPracticePractionersByPractice
{
    public class GetPracticePractionersByPracticeCommand : IRequest<Result<IEnumerable<UserPracticeDto>>>
    {
        public Guid PracticeId { get; set; }
    }
}
