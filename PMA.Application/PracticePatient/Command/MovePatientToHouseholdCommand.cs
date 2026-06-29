using Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.PracticePatient.Command
{
    public class MovePatientToHouseholdCommand : IRequest<Result<string>>
    {
        public string PatientId { get; set; }
        public string TargetHouseholdId { get; set; }
    }
}
