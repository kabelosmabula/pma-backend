using Microsoft.IdentityModel.Tokens;
using Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Service.Services
{
    public class JwtTokenService
    {
        private readonly string? _secretKey;
        public JwtTokenService(IConfiguration configuration)
        {
            _secretKey = configuration.GetValue<string?>("JwtSettings:SecretKey");
        }

        public void RemoveJwtToken(string token)
        {
            var securityHandler = new JwtSecurityTokenHandler();
            var currrentToken = securityHandler.ReadJwtToken(token);

            var claims = new List<Claim>(currrentToken.Claims);
            var existingClaim = claims.Find(c => c.Type == JwtRegisteredClaimNames.Exp);

            if (existingClaim != null)
            {
                claims.Remove(existingClaim);
            }

        }
        public string UpdateTokenExpiration(string token)
        {
            string newToken = string.Empty;

            var securityHandler = new JwtSecurityTokenHandler();
            var currrentToken = securityHandler.ReadJwtToken(token);
            var newExpiringTime = DateTime.Now.AddHours(1);

            var claims = new List<Claim>(currrentToken.Claims);
            var existingClaim = claims.Find(c => c.Type == JwtRegisteredClaimNames.Exp);

            if (existingClaim != null)
            {
                claims.Remove(existingClaim);
            }

            if (_secretKey != null)
            {
                claims.Add(new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(newExpiringTime).ToUnixTimeSeconds().ToString()));
                var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_secretKey));
                var tokenPrincipal = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var newJwtToken = new JwtSecurityToken(

                    claims: claims,
                    notBefore: currrentToken.ValidFrom,
                    expires: newExpiringTime,
                    signingCredentials: tokenPrincipal
                );

                newToken = securityHandler.WriteToken(newJwtToken);
            }

            return newToken;
        }
        public ActiveJwtToken ExtractJwtPayload(string token)
        {
            var securityHandler = new JwtSecurityTokenHandler();
            ActiveJwtToken payload = new ActiveJwtToken();

            if (_secretKey != null)
            {
                var key = Encoding.ASCII.GetBytes(_secretKey);

                var props = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };

                var tokenPrincipal = securityHandler.ValidateToken(token, props, out var securityToken);
                var currrentToken = (JwtSecurityToken)securityToken;

                var username = currrentToken.Payload.FirstOrDefault(x => x.Key == "given_name").Value.ToString();
                var userRole = currrentToken.Payload.FirstOrDefault(x => x.Key == "role").Value.ToString();
                var userId = currrentToken.Payload.FirstOrDefault(x => x.Key == "primarysid").Value.ToString();

                payload = new ActiveJwtToken
                {
                    UserRole = userRole,
                    Username = username,
                    UserID = Guid.Parse(userId),

                };

            }

            return payload;
        }

        public string GenerateToken(ActiveSession Session)
        {

            string tokenString = string.Empty;

            if (_secretKey != null)
            {

                var securityHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_secretKey);

                var props = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.PrimarySid, Session.UserID.ToString()),
                        new Claim(ClaimTypes.Role, Session.UserRole),
                        new Claim(ClaimTypes.GivenName, Session.DisplayName),
                        new Claim(ClaimTypes.Email, Session.Email)
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = securityHandler.CreateToken(props);
                tokenString = securityHandler.WriteToken(token);
            }

            return tokenString;

        }

    }

}
