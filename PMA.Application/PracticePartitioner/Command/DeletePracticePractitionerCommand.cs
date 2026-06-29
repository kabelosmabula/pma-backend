using Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.PracticePartitioner.Command
{
    public class DeletePracticePractitionerCommand : IRequest<Result<string>>
    {
        public string PracticePartitionerid { get; set; }
        public string DeletedBy { get; set; }
        public string userid { get; set; }
    }
}
