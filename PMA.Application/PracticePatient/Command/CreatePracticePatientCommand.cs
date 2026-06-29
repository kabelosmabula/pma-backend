using AutoMapper;
using Common;
using MediatR;
using PMA.Application.Interface.Mapping;
using PMA.Application.PracticePatient.Dtos;
using PMA.Application.PracticePatient.DTOs;
using PMA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.PracticePatient.Command
{
    public class CreatePracticePatientCommand : IRequest<Result<string>>
    {
        public string PracticeId { get; set; }
        public string? HouseholdId { get;  set; }
        public string firstName { get; set; } 
        public string lastName { get; set; }
        public string gender { get; set; } 
        public string idNumber { get; set; } 
        public string email { get; set; }
        public string phone { get; set; }
        public string dateOfBirth { get; set; }
        public string userid { get; set; }
        public AddressDto? Addresses { get; set; }
        public EmergencyContactDto emergencyContacts { get; set; }
        public MedicalAidDto MedicalAids { get; set; }
        public AllergiesAndAlertsDto? allergiesAndAlerts { get; set; }
    }
}
