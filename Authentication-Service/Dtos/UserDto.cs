namespace API.Service.Dtos
{
    public class UserDto
    {
        public string Firstname { get; set; } = null!;

        public string Lastname { get; set; } = null!;

        public string Displayname { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? Phonenumber { get; set; }

        public string Accountstatus { get; set; } = null!;

    }
}
