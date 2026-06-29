namespace API.Service.Dtos
{
    public class AccessDtoRoles
    {
        public Guid RoleId { get; set; }

        public string RoleName { get; set; } = string.Empty;

        public Guid PracticeId { get; set; }
    }
}
