using API.Service.Dtos;
using API.Service.Services;
using Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using PMA.Persistence;

namespace Authentication.Service.Features.Users.Command.CreateAuthenticationToken
{
    public class CreateAuthTokenHandler : IRequestHandler<CreateAuthTokenCommand, Result<AuthResponse>>
    {

        private readonly PMADBContext _dbContext;
        private readonly string? _secretKey;
        private readonly JwtTokenService _jwtTokenService;  

        public CreateAuthTokenHandler(JwtTokenService jwtTokenService, IConfiguration configuration, PMADBContext dbContext)
        {
            _dbContext = dbContext;
            _jwtTokenService = jwtTokenService; 
            _secretKey = configuration.GetValue<string?>("JwtSettings:SecretKey");
        }

        public async Task<Result<AuthResponse>> Handle(CreateAuthTokenCommand request, CancellationToken cancellationToken)
        {

            try
            {

                var user = await _dbContext.Users.Where(x => x.Email == request.Email.ToLowerInvariant()).FirstOrDefaultAsync() ?? throw new Exception($"No profile found with an email of {request.Email}");

                if (user.isdeleted == true) throw new Exception("Your account was deleted , please contact support");

                if (user.Isemailverified != true) throw new Exception("Your account is pending email verification");

                var userRole = await _dbContext.UserRoles.Where(x => x.UserId == user.id).Include(x => x.Role).FirstOrDefaultAsync() ?? throw new Exception("Sorry you don't have any role assingned to to your profile");

                var userPractices = await _dbContext.UserRoles
                                     .Where(x => x.UserId == user.id && x.PracticeId != null)
                                     .Include(x => x.Practice)
                                     .Include(x => x.Role)
                                     .ToListAsync();

                var listOfPractices = new List<PracticeAuthDto>();
                var listOfRoles = new List<AccessDtoRoles>();
              
                foreach (var practice in userPractices)
                {

                    listOfPractices.Add(new PracticeAuthDto
                    {
                        PracticeId = practice.Practice.id,
                        PracticeName = practice.Practice.Name,
                    });

                    listOfRoles.Add(new AccessDtoRoles
                    {
                        PracticeId = practice.Practice.id,
                        RoleId = practice.RoleId,
                        RoleName = practice.Role?.Name ?? "Unknown Role"
                    });
                }

                if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password)) { throw new Exception("Invalid username or password"); }

                var session = new ActiveSession
                {
                    DisplayName = user.Displayname,
                    Email = user.Email,
                    UserID = user.id,
                    UserRole = userRole.Role.Name
                };

                var userProfile = new AuthUserDto
                {
                    Id = user.id,
                    Email = user.Email,
                    FirstName = user.Firstname,
                    LastName = user.Lastname,
                    IsActive = user.Accountstatus == "Active" ? true : false,
                    Role = userRole.Role.Name,
                };

                string tokenString = _jwtTokenService.GenerateToken(session); 

                var autheResponse = new AuthResponse {User = userProfile ,Token = tokenString , Practices = listOfPractices ,Roles = listOfRoles };
                return Result<AuthResponse>.Ok(autheResponse);

            }
            catch (Exception ex)
            {
                return Result<AuthResponse>.Fail(ex.Message);
            }

        }
    }
}
