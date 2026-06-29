using API.Service.Dtos;
using API.Service.Features.Users.Command.UpdateAccountProfile;
using API.Service.Features.Users.Queries.GetAccountProfileByEmail;
using API.Service.Features.Users.Queries.GetAllRoles;
using API.Service.SharedModels;
using Authentication.Service.Helpers;
using Common;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace API.Service.Controllers
{
    [Authorize]
    [EnableCors("Domains")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly Helper _helper;    

        public UsersController(IMediator mediator, Helper helper)
        {
            _mediator = mediator;
            _helper = helper;   
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAccount([FromBody] CreateAccountShared body)
        {

            try
            {
                var session = _helper.GetActiveJwtToken(Request);
                var command = body.Adapt<UpdateAccountProfileCommand>(); command.UserId = session.ActiveJwtToken.UserID;
                var result = await _mediator.Send(command); if (!result.Success) throw new Exception(result.Error ?? "");

                return Ok(Result<string?>.Ok(result.Data));

            }
            catch (Exception ex)
            {
                return BadRequest(Result<string>.Fail(ex.Message));
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetByEmail([FromQuery] string email)
        {

            try
            {
                var result = await _mediator.Send(new GetAccountProfileByEmailQuery { Email = email,}); if (!result.Success) throw new Exception(result.Error ?? "");
                return Ok(Result<UserDto?>.Ok(result.Data));
            }

            catch (Exception ex)
            {
                return BadRequest(Result<string>.Fail(ex.Message));
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {

            try
            {
                var result = await _mediator.Send(new GetAllRolesQuery()); if (!result.Success) throw new Exception(result.Error ?? "");
                return Ok(Result<IEnumerable<RolesDto?>>.Ok(result.Data ?? new List<RolesDto?>()));
            }

            catch (Exception ex)
            {
                return BadRequest(Result<string>.Fail(ex.Message));
            }

        }
    }
}
