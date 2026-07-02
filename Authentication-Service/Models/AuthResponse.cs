using System.Text.Json.Serialization;
using API.Service.Dtos;

namespace Models
{
    public class AuthResponse
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Token { get; set; } = string.Empty;

        public AuthUserDto User { get; set; } = new AuthUserDto();

        public List<AccessDtoRoles> Roles { get; set; } = new List<AccessDtoRoles>(); 

        public List<PracticeAuthDto> Practices { get; set; } = new List<PracticeAuthDto>(); 

    };
}
