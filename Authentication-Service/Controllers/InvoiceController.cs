using API.Service.Dtos;
using API.Service.Features.Invoices.Commands.PayInvoice;
using API.Service.Features.Invoices.CreateInvoice.Commands.CreateInvoice;
using API.Service.Features.Invoices.Queries.GetInvoiceById;
using API.Service.Features.Invoices.Queries.GetInvoicesByPractice;
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
    public class InvoiceController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly Helper _helper;

        public InvoiceController(IMediator mediator , Helper helper)
        {
            _mediator = mediator;
            _helper = helper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateInvoice([FromBody] InvoiceShared body)
        {

            try
            {
                var session = _helper.GetActiveJwtToken(Request);
                var command = body.Adapt<CreateInvoiceCommand>(); command.UserId = session.ActiveJwtToken.UserID;
                var result = await _mediator.Send(command); if (!result.Success) throw new Exception(result.Error ?? "");

                return Ok(Result<string?>.Ok(result.Data));

            }
            catch (Exception ex)
            {
                return BadRequest(Result<string>.Fail(ex.Message));
            }

        }

        [HttpPost]
        public async Task<IActionResult> PayInvoice([FromBody] PayInvoiceShared body)
        {

            try
            {
                var session = _helper.GetActiveJwtToken(Request);
                var command = body.Adapt<PayInvoiceCommand>(); command.UserId = session.ActiveJwtToken.UserID;
                var result = await _mediator.Send(command); if (!result.Success) throw new Exception(result.Error ?? "");

                return Ok(Result<string?>.Ok(result.Data));

            }
            catch (Exception ex)
            {
                return BadRequest(Result<string>.Fail(ex.Message));
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetInvoiceById([FromQuery] Guid invoiceId)
        {
            try
            {
                var command = new GetInvoiceByIdQuery() { InvoiceId = invoiceId };
                var result = await _mediator.Send(command); if (!result.Success) throw new Exception(result.Error ?? "");
                return Ok(Result<InvoiceDto?>.Ok(result.Data));

            }
            catch (Exception ex)
            {
                return BadRequest(Result<string>.Fail(ex.Message));
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetInvoicesByPractice([FromQuery] Guid PracticeId)
        {
            try
            {
                var command = new GetInvoicesByPracticeQuery() { PraticeId = PracticeId };
                var result = await _mediator.Send(command); if (!result.Success) throw new Exception(result.Error ?? "");
                return Ok(Result<List<InvoiceOnlyDto>>.Ok(result.Data ?? new List<InvoiceOnlyDto>()));

            }
            catch (Exception ex)
            {
                return BadRequest(Result<string>.Fail(ex.Message));
            }
        }
    }
}
