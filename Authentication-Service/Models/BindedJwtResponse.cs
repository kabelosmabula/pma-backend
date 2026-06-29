using Models;

namespace API.Service.Models
{
    public class BindedJwtResponse
    {
        public string Token { get; set; } = string.Empty;

        public ActiveJwtToken ActiveJwtToken { get; set; } = new ActiveJwtToken();
    }
}
