using AutoMapper;
using Common;
using MediatR;
using PMA.Application.Interface.Mapping;
using PMA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.PracticePatient.Command
{
    public class RemovePatientFromHouseholdCommand : IRequest<Result<string>>, IHaveCustomMapping
    {
        public string PracticeId { get; set; }
        public string HouseholdId { get; set; }
        public string PatientId { get; set; }

        public string? NewHouseholdId { get; set; }
        public bool CreateNewHousehold { get; set; }
        public string? HouseholdName { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<RemovePatientFromHouseholdCommand, Patient>();
        }
    }
}
