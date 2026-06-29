using API.Service.Dtos;

namespace Models
{
    public class AuthResponse
    {
        public string Token { get; set; } = string.Empty;

        public Guid RefreshToken { get; set; }

        public AuthUserDto User { get; set; } = new AuthUserDto();

        public List<AccessDtoRoles> Roles { get; set; } = new List<AccessDtoRoles>(); 

        public List<PracticeAuthDto> Practices { get; set; } = new List<PracticeAuthDto>(); 

    };
}
