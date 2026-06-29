using Authentication.Service.Helpers;
using Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMA.Domain.Entities;
using PMA.Persistence;
using Services;

namespace API.Service.Features.MedicalPractice.Command.AddPracticePractioner
{
    public class AddPracticePractionerHandler : IRequestHandler<AddPracticePractionerCommand, Result<string>>
    {
        private readonly Helper _helper;
        private readonly PMADBContext _dbContext;
        private readonly EmailService _emailService;


        public AddPracticePractionerHandler(PMADBContext dbContext , Helper helper , EmailService emailService)
        {
            _helper = helper;   
            _dbContext = dbContext;
            _emailService = emailService;
        }

        public async Task<Result<string>> Handle(AddPracticePractionerCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var email = await _dbContext.Users.Where(x => x.Email == request.Email.ToLowerInvariant()).FirstOrDefaultAsync();

                if (email is not null) throw new Exception($"{email.Displayname} already exist , if the user is not part of your practice , link them using this email {request.Email.ToLowerInvariant()} ");

                var medicalPractice = await _dbContext.Practice.Where(x => x.id == request.PracticeId).FirstOrDefaultAsync() ?? throw new Exception("Practice not found");

                var user = await _dbContext.Users.Where(x => x.id == request.UserId).FirstOrDefaultAsync() ?? throw new Exception("User not found");

                var role = await _dbContext.Roles.Where(x => x.id == request.RoleId).FirstOrDefaultAsync() ?? throw new Exception("Role not found");

                var userId = Guid.NewGuid();
                string password = _helper.GeneratePassword();

                var profile = new User
                {
                    id = userId,
                    Firstname = request.Firstname,
                    Lastname = request.Lastname,
                    Email = request.Email.ToLowerInvariant(),
                    Password = BCrypt.Net.BCrypt.HashPassword(password.ToString()),
                    createddate = DateTime.UtcNow,
                    createdby = userId.ToString(),
                    Isemailverified = true,
                    Istwofactorenabled = true,
                    Displayname = request.Firstname + " " + request.Lastname,
                    Accountstatus = "Verified",
                };

                await _dbContext.AddAsync(profile);

                var accessRole = new UserRole { PracticeId = medicalPractice.id,id = Guid.NewGuid(), UserId = userId, RoleId = role.id, createddate = DateTime.UtcNow, Isactive = true, createdby = userId.ToString() };

                var userPractice = new UserPractice { id = Guid.NewGuid(), UserId = user.id, PracticeId = medicalPractice.id, createddate = DateTime.UtcNow, isActive = true, createdby = request.UserId.ToString() };

                await _dbContext.AddAsync(accessRole);
                await _dbContext.AddAsync(userPractice);

                await _dbContext.SaveChangesAsync();

                var template = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "InternalPractionerRegistration.html");

                var emailTosend = File.ReadAllText(template);

                emailTosend = emailTosend.Replace("[DisplayName]", profile.Displayname);
                emailTosend = emailTosend.Replace("[PracticeName]", medicalPractice.Name);
                emailTosend = emailTosend.Replace("[Password]", password);

                var results = await _emailService.SendEmail(profile.Email.ToLowerInvariant(), "Verification Code", emailTosend);

                if (!results.Success) throw new Exception(results.Message);

                return Result<string>.Ok($"Your profile was created successfully , and an email was sent to you with accont credentials");
            }
            catch (Exception ex)
            {
                return Result<string>.Fail(ex.Message);
            }
        }
    }
}
