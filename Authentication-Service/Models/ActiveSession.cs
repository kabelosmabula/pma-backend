namespace Models
{
    public class ActiveSession
    {
        public string UserRole { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string DisplayName { get; set; } = string.Empty;

        public Guid UserID { get; set; }
    }
}
