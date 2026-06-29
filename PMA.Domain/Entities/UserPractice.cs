using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Domain.Entities
{

    public class UserPractice:BaseEntity
    {
        public Guid UserId { get;  set; }
        public User User { get;  set; }
        public Guid PracticeId { get;  set; }
        public bool isActive { get; set; }
        public Practice Practice { get;  set; }
    }
}
