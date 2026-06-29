using API.Service.SharedModels;
using Common;
using MediatR;
using System.ComponentModel.DataAnnotations;


namespace PMA.Application.Features.MedicalPractice.Command.AddMedicalPractice
{
    public class AddMedicalPracticeCommand : MedicalPracticeShared, IRequest<Result<string>> 
    {
        [Required]
        public Guid UserId { get; set; }
    } 
}
