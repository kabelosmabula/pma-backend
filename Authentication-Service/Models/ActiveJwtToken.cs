namespace Models
{
    public class ActiveJwtToken
    {
        public string UserRole { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public Guid UserID { get; set; }
    }
}
