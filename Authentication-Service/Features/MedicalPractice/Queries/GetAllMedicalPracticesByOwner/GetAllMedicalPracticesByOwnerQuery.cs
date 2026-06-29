using Common;
using MediatR;
using PMA.Application.Dtos;
using System.ComponentModel.DataAnnotations;

namespace PMA.Application.Features.GetAllMedicalPracticesByOwner.Queries.GetPracticesByOwner
{
    public class GetAllMedicalPracticesByOwnerQuery : IRequest<Result<IEnumerable<PracticeDto?>>>
    {
        [Required]
        public Guid UserId { get; set; } 
    }
}
