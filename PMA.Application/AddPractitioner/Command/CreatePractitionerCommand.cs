using Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.AddPractitioner.Command
{
    public class CreatePractitionerCommand : IRequest<Result<string>>
    {
        public string UserId { get; set; }
        public string HPCSANumber { get; set; }
        public string Fullname { get; set; }
        public int Yearsofexperience { get; set; }
        public DateTime Licenseexpirydate { get; set; } 
    }
}
