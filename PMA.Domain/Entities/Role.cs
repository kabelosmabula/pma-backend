using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Domain.Entities
{
    public class Role : BaseEntity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
    }
}
