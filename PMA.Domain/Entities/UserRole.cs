using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Domain.Entities
{
    public class UserRole:BaseEntity
    {
        public Guid UserId { get;  set; }
        public User User { get;  set; } =  null!;
        public Guid RoleId { get;  set; }
        public Role Role { get; set; } = null!;
        public bool Isactive { get; set; }
        public Guid? PracticeId { get;  set; }
        public Practice? Practice { get; set; }
    }
}
