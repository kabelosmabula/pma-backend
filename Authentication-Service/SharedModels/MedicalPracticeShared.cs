using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace API.Service.SharedModels
{
    public class MedicalPracticeShared
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string PracticeNumber { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;

        [Required]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
