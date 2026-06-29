using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.Dtos
{
    public class PracticeDto
    {
        public Guid id { get; set; }
        public string Name { get; set; }  = string.Empty;
        public string PracticeNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
