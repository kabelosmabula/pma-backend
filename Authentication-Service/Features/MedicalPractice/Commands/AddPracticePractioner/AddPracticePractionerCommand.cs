using API.Service.SharedModels;
using Common;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace API.Service.Features.MedicalPractice.Command.AddPracticePractioner
{
       
    public class AddPracticePractionerCommand :CreatePracticePractionerShared, IRequest<Result<string>>
    {
        [Required]
        public Guid UserId { get; set; }

    }
}
