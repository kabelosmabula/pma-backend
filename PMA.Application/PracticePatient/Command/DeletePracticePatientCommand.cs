using Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.PracticePatient.Command
{
    public class DeletePracticePatientCommand : IRequest<Result<string>>
    {
        public string patientid { get; set; }
        public string PracticeId { get; set; }
        public string Displayname { get; set; }
    }
}
