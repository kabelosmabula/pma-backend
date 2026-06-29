using Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.PracticePatient.Command
{
    public class UpdatePatientHouseholdCommand : IRequest<Result<string>>
    {
        public string HouseholdId { get; set; }
        public string HouseholdName { get; set; }
        public string PrimaryContactName { get; set; }
        public string PrimaryContactSurName { get; set; }
        public string PrimaryContactPhone { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }
        public List<UpdatePatientCommand> Patients { get; set; } = new();
    }
    public class UpdatePatientCommand
    {
        public string PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Displayname { get; set; }
        public string Identitynumber { get; set; }
        public string? Email { get; set; }
        public string Phonenumber { get; set; }
        public DateOnly? Dateofbirth { get; set; }
    }
}