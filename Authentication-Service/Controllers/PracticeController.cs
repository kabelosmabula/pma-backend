using API.Service.Dtos;
using API.Service.Features.Invoices.Commands.PayInvoice;
using API.Service.Features.MedicalPractice.Command.AddPracticePractioner;
using API.Service.Features.MedicalPractice.Command.DeleteMedicalPractice;
using API.Service.Features.MedicalPractice.Command.LinkPracticePractioner;
using API.Service.Features.MedicalPractice.Commands.UnlinkPracticePractioner;
using API.Service.Features.MedicalPractice.Queries.GetPracticePractionersByPractice;
using API.Service.Features.Users.Command.ManagePracticeAccessStatus;
using API.Service.SharedModels;
using Authentication.Service.Helpers;
using Common;
using DocumentFormat.OpenXml.Spreadsheet;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PMA.Application.Dtos;
using PMA.Application.Features.GetAllMedicalPracticesByOwner.Queries.GetPracticesByOwner;
using PMA.Application.Features.MedicalPractice.Command.AddMedicalPractice;
using PMA.Application.Features.MedicalPractice.Command.UpdateMedicalPractice;
using PMA.Application.Features.MedicalPractice.Queries.GetMedicalPracticeById;
using PMA.Domain.Entities;

namespace API.Service.Controllers
{
    [Authorize]
    [EnableCors("Domains")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PracticeController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly Helper _helper;

        public PracticeController(IMediator mediator, Helper helper)
        {
            _mediator = mediator;
            _helper = helper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MedicalPracticeShared body)
        {
            try
            {
                var session = _helper.GetActiveJwtToken(Request);
                var command = body.Adapt<AddMedicalPracticeCommand>(); command.UserId = session.ActiveJwtToken.UserID;
                var result = await _mediator.Send(command); if (!result.Success) throw new Exception(result.Error ?? "");

                return Ok(Result<string?>.Ok(result.Data));

            }
            catch (Exception ex)
            {
                return BadRequest(Result<string>.Fail(ex.Message));
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] MedicalPracticeShared body)
        {

            try
            {
                var session = _helper.GetActiveJwtToken(Request);
                var command = body.Adapt<UpdateMedicalPracticeCommand>(); command.id = id; command.UserId = session.ActiveJwtToken.UserID;
                var result = await _mediator.Send(command); if (!result.Success) throw new Exception(result.Error ?? "");


                return Ok(Result<string?>.Ok(result.Data));

            }
            catch (Exception ex)
            {
                return BadRequest(Result<string>.Fail(ex.Message));
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] Guid practiceId)
        {
            try
            {
                var session = _helper.GetActiveJwtToken(Request);
                var result = await _mediator.Send(new DeleteMedicalPracticeCommand { UserId = session.ActiveJwtToken.UserID, PracticeId = practiceId }); if (!result.Success) throw new Exception(result.Error ?? "");
                return Ok(Result<string?>.Ok(result.Data));

            }
            catch (Exception ex)
            {
                return BadRequest(Result<string>.Fail(ex.Message));
            }
        }

        [HttpGet("{practiceId}")]
        public async Task<IActionResult> GetPractionersByPracticeId(Guid practiceId)
        {

            try
            {
                var result = await _mediator.Send(new GetPracticePractionersByPracticeCommand { PracticeId = practiceId }); if (!result.Success) throw new Exception(result.Error ?? "");
                return Ok(Result<IEnumerable<UserPracticeDto?>>.Ok(result.Data ?? new List<UserPracticeDto?>()));

            }
            catch (Exception ex)
            {
                return BadRequest(Result<string>.Fail(ex.Message));
            }
        }


        [HttpPost]
        public async Task<IActionResult> LinkPractioner([FromQuery] Guid PracticeId , [FromQuery] Guid RoleId , [FromQuery] string Email )
        {
            try
            {
                var session = _helper.GetActiveJwtToken(Request);
                var result = await _mediator.Send(new LinkPracticePractionerCommand { PracticeId = PracticeId , RoleId = RoleId ,Email = Email , UserId = session.ActiveJwtToken.UserID }); if (!result.Success) throw new Exception(result.Error ?? "");
                return Ok(Result<string?>.Ok(result.Data));

            }
            catch (Exception ex)
            {
                return BadRequest(Result<string>.Fail(ex.Message));
            }
        }


        [HttpPost]
        public async Task<IActionResult> UnlinkPractioner([FromQuery] string Email,[FromQuery] Guid PracticeId)
        {
            try
            {
                var session = _helper.GetActiveJwtToken(Request);
                var result = await _mediator.Send(new UnlinkPracticePractionerCommand { PracticeId = PracticeId, UserId = session.ActiveJwtToken.UserID, Email = Email }); if (!result.Success) throw new Exception(result.Error ?? "");
                return Ok(Result<string?>.Ok(result.Data));

            }
            catch (Exception ex)
            {
                return BadRequest(Result<string>.Fail(ex.Message));
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetAllPracticesByOwner()
        {
            try
            {
                var session = _helper.GetActiveJwtToken(Request);

                var result = await _mediator.Send(new GetAllMedicalPracticesByOwnerQuery { UserId = session.ActiveJwtToken.UserID }); if (!result.Success) throw new Exception(result.Error ?? "");
                return Ok(Result<IEnumerable<PracticeDto?>>.Ok(result.Data ?? new List<PracticeDto?>()));

            }
            catch (Exception ex)
            {
                return BadRequest(Result<string>.Fail(ex.Message));
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetByPracticeId([FromQuery] Guid practiceId)
        {
            try
            {
                var result = await _mediator.Send(new GetMedicalPracticeByIdQuery { PracticeId = practiceId }); if (!result.Success) throw new Exception(result.Error ?? "");
                return Ok(Result<PracticeDto?>.Ok(result.Data));

            }
            catch (Exception ex)
            {
                return BadRequest(Result<string>.Fail(ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddPracticePractionerProfile([FromBody] CreatePracticePractionerShared body)
        {

            try
            {
                var session = _helper.GetActiveJwtToken(Request);
                var command = body.Adapt<AddPracticePractionerCommand>(); command.UserId = session.ActiveJwtToken.UserID;
                var result = await _mediator.Send(command); if (!result.Success) throw new Exception(result.Error ?? "");

                return Ok(Result<string?>.Ok(result.Data));

            }
            catch (Exception ex)
            {
                return BadRequest(Result<string>.Fail(ex.Message));
            }

        }

        [HttpPut("{practiceId}/{isActive}")]
        public async Task<IActionResult> ManagePracticeAccessStatus(Guid practiceId, bool isActive)
        {

            try
            {
                var session = _helper.GetActiveJwtToken(Request);
                var result = await _mediator.Send(new ManagePracticeAccessStatusCommand { IsActive = isActive, PracticeId = practiceId, UserId = session.ActiveJwtToken.UserID }); if (!result.Success) throw new Exception(result.Error ?? "");
                return Ok(Result<string?>.Ok(result.Data));

            }
            catch (Exception ex)
            {
                return BadRequest(Result<string>.Fail(ex.Message));
            }

        }


    }
}
