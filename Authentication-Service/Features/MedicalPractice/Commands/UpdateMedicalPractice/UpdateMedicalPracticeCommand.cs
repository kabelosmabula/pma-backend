using API.Service.SharedModels;
using Common;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace PMA.Application.Features.MedicalPractice.Command.UpdateMedicalPractice
{
    public class UpdateMedicalPracticeCommand : MedicalPracticeShared, IRequest<Result<string>>
    {
        [Required]
        public  Guid id { get; set; }

        [Required]
        public Guid UserId { get; set; }
    } 
   
}
