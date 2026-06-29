using AutoMapper;
using Common;
using MediatR;
using PMA.Application.Interface.Mapping;
using PMA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.PracticePatient.Command.PatientClinicalRecord
{
    public class CreateClinicalRecordCommand : IRequest<Result<string>>, IHaveCustomMapping
    {
        public Guid PracticeId { get; set; }
        public Guid PatientId { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<CreateClinicalRecordCommand,ClinicalRecord>();
        }
    }
}
