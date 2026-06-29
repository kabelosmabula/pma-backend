using Common;
using MediatR;
using PMA.Application.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PMA.Application.Features.MedicalPractice.Queries.GetMedicalPracticeById
{
    public class GetMedicalPracticeByIdQuery : IRequest<Result<PracticeDto>>
    {
        [Required]
        public Guid PracticeId { get; set; }
    }
}
