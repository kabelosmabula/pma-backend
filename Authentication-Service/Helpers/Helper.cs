using API.Service.Models;
using API.Service.Services;
using Models;

namespace Authentication.Service.Helpers
{
    public class Helper
    {
        private readonly JwtTokenService _jwtTokenService;

        public Helper(JwtTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
        }

        private static readonly Random _random = new Random();

        public int GenerateFiveDigitNumber()
        {
            return _random.Next(10000, 100000);
        }

        public string GeneratePassword()
        {
            int length = 8;
            const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz23456789";

            char[] password = new char[length];

            for (int i = 0; i < length; i++)
            {
                password[i] = chars[_random.Next(chars.Length)];
            }

            return new string(password);
        }


        public BindedJwtResponse GetActiveJwtToken(HttpRequest request)
        {
            var authorization = request.Headers["Authorization"].FirstOrDefault(); if (authorization is null || !authorization.StartsWith("Bearer ")) throw new Exception("Token not found");
            var token = authorization["Bearer ".Length..].Trim();

            return new BindedJwtResponse { Token = token, ActiveJwtToken = _jwtTokenService.ExtractJwtPayload(token) };
        }

    }

}
