using Authentication.Service.Helpers;
using Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PMA.Domain.Entities;
using PMA.Persistence;
using Services;

namespace PMA.Application.Features.Users.Command.CreateAccount
{
    public class CreateAccountHandler : IRequestHandler<CreateAccountCommand, Result<string>>
    {

        private readonly PMADBContext _dbContext;
        private readonly EmailService _emailService;

        private readonly string? _frontEndUrl = string.Empty;

        public CreateAccountHandler(PMADBContext dbContext , IConfiguration configuration , EmailService emailService )
        {
            _dbContext = dbContext;
            _emailService = emailService;
            _frontEndUrl = configuration.GetValue<string>("FrontEndUrl");
        }

        public async Task<Result<string>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {

            using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

 
            try
            {

                if ((await _dbContext.Users.Where(x => x.Email == request.Email.ToLowerInvariant()).FirstOrDefaultAsync()) != null) throw new Exception($"User {request.Email} already exist");

                var role = await _dbContext.Roles.Where(x => x.Name == "User").FirstOrDefaultAsync() ?? throw new Exception("Requested role not found");

                var userId = Guid.NewGuid();

                var profile = new User
                {
                    id = userId,
                    Firstname = request.Firstname,
                    Lastname = request.Lastname,
                    Email = request.Email.ToLowerInvariant(),
                    Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                    createddate = DateTime.UtcNow,
                    createdby = userId.ToString(),
                    Isemailverified = false,
                    Istwofactorenabled = true,
                    Displayname = request.Firstname + " " + request.Lastname,
                    Accountstatus = "Pending",
                };

                await _dbContext.AddAsync(profile);

                var accessRole = new UserRole { id = Guid.NewGuid(), UserId = userId, RoleId = role.id, createddate = DateTime.UtcNow, Isactive = true, createdby = userId.ToString() };

                await _dbContext.AddAsync(accessRole);

                await _dbContext.SaveChangesAsync();

                var url = $"{_frontEndUrl}verify?email={profile.Email}&userid={profile.id}";

                var template = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "VerifyEmail.html");

                var emailTosend = File.ReadAllText(template);

                emailTosend = emailTosend.Replace("[URL]", url);

                var results = await _emailService.SendEmail(profile.Email.ToLowerInvariant(), "Verification Code", emailTosend);

                if (!results.Success) throw new Exception(results.Message);

                await transaction.CommitAsync();

                return Result<string>.Ok($"Your profile was created successfully , and an email was sent to you to verify your account");

            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                return Result<string>.Fail(ex.Message);
                
            }
        }
    }
}
