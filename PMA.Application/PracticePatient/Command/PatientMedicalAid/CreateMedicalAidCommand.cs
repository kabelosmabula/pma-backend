using AutoMapper;
using Common;
using MediatR;
using PMA.Application.Interface.Mapping;
using PMA.Application.PracticePatient.Command.PatientClinicalRecord;
using PMA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.PracticePatient.Command.PatientMedicalAid
{
    public class CreateMedicalAidCommand : IRequest<Result<string>>, IHaveCustomMapping
    {
        public Guid PatientId { get; set; }
        public string SchemeCode { get; set; }
        public string SchemeName { get; set; }
        public string PlanCode { get; set; }
        public string PlanName { get; set; }
        public string MembershipNumber { get; set; }
        public string DependentCode { get; set; }
        public bool IsActive { get; set; }
        public DateOnly EffectiveFrom { get; set; }
        public DateOnly? EffectiveTo { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<CreateMedicalAidCommand, MedicalAid>();
        }
    }
}
