using Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMA.Application.AddPractitioner.Command;
using PMA.Domain.Entities;
using PMA.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.AddPractitioner.Handler
{
    public class CreatePractitionerCommandHandler
    : IRequestHandler<CreatePractitionerCommand, Result<string>>
    {
        private readonly PMADBContext _context;

        public CreatePractitionerCommandHandler(PMADBContext context)
        {
            _context = context;
        }

        public async Task<Result<string>> Handle(CreatePractitionerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(x => x.id == Guid.Parse(request.UserId) && !x.isdeleted, cancellationToken);

                if (user == null)
                    return Result<string>.Fail("User not found");

                var exists = await _context.Practitioners
                    .AnyAsync(x => x.HPCSANumber == request.HPCSANumber && !x.isdeleted, cancellationToken);

                if (exists)
                    return Result<string>.Fail("Practitioner with this HPCSA already exists");

                var expiryDate = DateOnly.FromDateTime(request.Licenseexpirydate);

                var practitioner = new Practitioner
                {
                    UserId = Guid.Parse(request.UserId),
                    HPCSANumber = request.HPCSANumber,
                    Fullname = request.Fullname,
                    Yearsofexperience = request.Yearsofexperience,
                    Licenseexpirydate = expiryDate,
                    createddate = DateTime.UtcNow,
                    createdby = user.Email 
                };

                await _context.Practitioners.AddAsync(practitioner, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return Result<string>.Ok("Practitioner created successfully");
            }
            catch (Exception ex)
            {
                return Result<string>.Fail(ex.Message);
            }
        }
    }
}
