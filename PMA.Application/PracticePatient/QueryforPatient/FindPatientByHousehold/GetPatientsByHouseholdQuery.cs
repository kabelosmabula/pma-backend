using Common;
using MediatR;
using PMA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.PracticePatient.QueryforPatient.FindPatientByHousehold
{
    public class GetPatientsByHouseholdQuery : IRequest<Result<List<Patient>>>
    {
        public string HouseholdId { get; set; }
        public string PracticeId { get; set; }
    }
}
