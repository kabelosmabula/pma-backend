using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Domain.Entities
{
    public class NappiItem : BaseEntity
    {
        public string Code { get; set; }
        public string Dosage { get; set; }
       // public string PackSize { get; set; }
        public string Name { get; set; }
    }
}
