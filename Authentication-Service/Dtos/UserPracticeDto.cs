namespace API.Service.Dtos
{
    public class UserPracticeDto
    {
        public string Firstname { get; set; } = null!;

        public string Lastname { get; set; } = null!;

        public string Displayname { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? Phonenumber { get; set; }

        public bool Isactive { get; set; }

    }
}
