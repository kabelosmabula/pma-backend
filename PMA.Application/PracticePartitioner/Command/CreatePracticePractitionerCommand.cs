using Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.PracticePartitioner.Command
{
    public class CreatePracticePractitionerCommand : IRequest<Result<string>>
    {
        public string PracticeId { get; set; }
        public string PractitionerId { get; set; }
        public string Specialty { get; set; }
        public string PracticeEmail { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; }
    }
}
