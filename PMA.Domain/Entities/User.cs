
namespace PMA.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Firstname { get; set; } = null!;

        public string Lastname { get; set; } = null!;

        public string Displayname { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public bool Isemailverified { get; set; }

        public bool Istwofactorenabled { get; set; }

        public string? Phonenumber { get; set; }

        public bool Isphonenumberverified { get; set; }

        public DateTime? Lastlogin { get; set; }

        public int Failedloginattempts { get; set; }

        public string Accountstatus { get; set; } = null!;

        public DateTime? Passwordchangedat { get; set; }

        public string? Lastotp { get; set; }

        public ICollection<UserRole> UserRoles { get; private set; } = new List<UserRole>();
        public ICollection<UserPractice> UserPractices { get; private set; } = new List<UserPractice>();
    }
}
