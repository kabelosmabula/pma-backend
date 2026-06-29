using Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.PracticePartitioner.Command
{
    public class UpdatePracticePractitionerCommand : IRequest<Result<string>>
    {
        public string PracticePartitionerId { get; set; }
        public string Specialty { get; set; }
        public string PracticeEmail { get; set; }
        public string StartDate { get; set; } 
        public string? EndDate { get; set; }
        public bool Isactive { get; set; }
        public string userid { get; set; }
    }
}
