using API.Service.Services;
using Authentication.Service.Features.Users.Command.CreateAuthenticationToken;
using Authentication.Service.Features.Users.Command.VerifyAccount;
using Authentication.Service.Helpers;
using Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Models;
using PMA.Application.Features.Users.Command.CreateAccount;

namespace API.Service.Controllers
{
    [Authorize]
    [EnableCors("Domains")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly JwtTokenService _jwtTokenService;
        private readonly Helper _helper;

        public AuthenticationController(IMediator mediator , JwtTokenService jwtTokenService, Helper helper)
        {
            _mediator = mediator;
            _jwtTokenService = jwtTokenService;
            _helper = helper;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] CreateAuthTokenCommand command)
        {

            try
            {
                var result = await _mediator.Send(command); if (!result.Success) throw new Exception(result.Error ?? "");
                Response.Headers.Append("Authorization", $"Bearer {result.Data.Token}"); result.Data.Token = null;
                return Ok(Result<AuthResponse?>.Ok(result.Data));

            }
            catch (Exception ex)
            {
                return BadRequest(Result<string>.Fail(ex.Message));
            }

        }

        [HttpGet]
        public IActionResult LogOut()
        {
            try
            {
                var session = _helper.GetActiveJwtToken(Request);
                _jwtTokenService.RemoveJwtToken(session.Token);

                return Ok(Result<string>.Ok("Successfully logged out"));

            }
            catch (Exception ex)
            {
                return BadRequest(Result<string>.Fail(ex.Message));
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountCommand command)
        {

            try
            {
                var result = await _mediator.Send(command); if (!result.Success) throw new Exception(result.Error ?? "");
                return Ok(Result<string?>.Ok(result.Data));
            }

            catch (Exception ex)
            {
                return BadRequest(Result<string>.Fail(ex.Message));
            }

        }

        [HttpPut]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyAccount([FromQuery] string email, [FromQuery] Guid userId)
        {

            try
            {
                var result = await _mediator.Send(new VerifyAccountCommand { email = email, userId = userId }); if (!result.Success) throw new Exception(result.Error ?? "");
                return Ok(Result<string?>.Ok(result.Data));
            }

            catch (Exception ex)
            {
                return BadRequest(Result<string>.Fail(ex.Message));
            }

        }

        [HttpGet]
        public IActionResult RefreshToken()
        {

            try
            {
                var session = _helper.GetActiveJwtToken(Request);
                string tokenString = _jwtTokenService.UpdateTokenExpiration(session.Token);
                Response.Headers.Append("Authorization", $"Bearer {tokenString}");

                return Ok(Result<string>.Ok("Token was updated"));

            }
            catch (Exception ex)
            {
                return BadRequest(Result<string>.Fail(ex.Message));
            }
        }

        [HttpGet]
        public IActionResult GetTokenPayload()
        {

            try
            {
                var session = _helper.GetActiveJwtToken(Request);
                return Ok(Result<ActiveJwtToken?>.Ok(session.ActiveJwtToken));

            }
            catch (Exception ex)
            {
                return BadRequest(Result<string>.Fail(ex.Message));
            }
        }

    }
}
