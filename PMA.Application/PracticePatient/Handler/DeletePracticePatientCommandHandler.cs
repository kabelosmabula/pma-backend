using AutoMapper;
using Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMA.Application.Exceptions;
using PMA.Application.PracticePatient.Command;
using PMA.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.PracticePatient.Handler
{
    public class DeletePracticePatientCommandHandler : IRequestHandler<DeletePracticePatientCommand, Result<string>>
    {
        private readonly PMADBContext _context;
        private readonly IMapper _mapper;
        public DeletePracticePatientCommandHandler(PMADBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Result<string>> Handle(DeletePracticePatientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var patientsummary = await _context.Patients
                   .FirstOrDefaultAsync(x => x.id == Guid.Parse(request.patientid) && x.PracticeId == Guid.Parse(request.PracticeId) && x.isdeleted == false, cancellationToken);
                if (patientsummary == null)
                    throw new FailedToFindException(nameof(Result<DeletePracticePatientCommand>), "Patient file does not exist ");
                patientsummary.isdeleted = true;
                patientsummary.deleteddate = DateTime.Now;
                patientsummary.deletedby = request.Displayname;
                await _context.SaveChangesAsync(cancellationToken);
                return Result<string>.Ok("Patient deleted from practice "+System.Text.Json.JsonSerializer.Serialize(patientsummary));
            }
            catch (Exception ex)
            {
                return Result<string>.Fail(ex.Message);
            }
        }
    }
}
