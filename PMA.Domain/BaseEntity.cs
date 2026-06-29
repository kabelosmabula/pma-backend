using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Domain
{
    public class BaseEntity
    {
        public Guid id { get; set; }
        public DateTime createddate { get; set; }
        public string createdby { get; set; }
        public DateTime? modifieddate { get; set; }
        public string modifiedby { get; set; }
        public bool isdeleted { get; set; }
        public DateTime? deleteddate { get; set; }
        public string deletedby { get; set; }
    }
}
