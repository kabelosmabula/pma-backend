using API.Service.Features.Users.Queries.GetAccountProfileByEmail;
using Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PMA.Application.AddPractitioner.Command;
using PMA.Application.PatientAppointments.Commands;
using PMA.Application.PatientAppointments.Queries.FindAllAppointmentForPatient;
using PMA.Application.PatientAppointments.Queries.FindAppointmentById;
using PMA.Application.PatientConsultation.Commands;
using PMA.Application.PatientConsultation.Queries.GetAllConsultationsforPractice;
using PMA.Application.PatientConsultation.Queries.GetConsultationById;
using PMA.Application.PracticePartitioner.Command;
using PMA.Application.PracticePatient.Command;
using PMA.Application.PracticePatient.QueryforPatient.FindPatientByHousehold;
using PMA.Application.PracticePatient.QueryforPatient.FindPatientById;
using PMA.Domain.Entities;

namespace API.Service.Controllers
{
    
    [EnableCors("Domains")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PracticesPatientController : ControllerBase
    {

        private readonly IMediator _mediator; 

        public PracticesPatientController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        [HttpPost("add-Practitioner")]
        public async Task<IActionResult> addPractitioner([FromBody] CreatePractitionerCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
        [HttpPost("create-Practice-Practitioner")]
        public async Task<IActionResult> CreatePracticePractitioner([FromBody] CreatePracticePractitionerCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
        [HttpPost("create-Practice-consultation")]
        public async Task<IActionResult> CreatePracticeConsultation([FromBody] CreateConsultationCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
        [HttpPost("update-Practice-consultation")]
        public async Task<IActionResult> UpdatePracticeConsultation([FromBody] UpdateConsultationCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
        [HttpDelete("Delete-Practice-Consultation")]
        public async Task<IActionResult> DeletePracticeConsultation([FromBody] DeleteConsultationCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
        [HttpGet("getall-Practice-consultation")]
        public async Task<IActionResult> getPracticeConsultation([FromQuery] string Practiceid)
        {
            var result = await _mediator.Send(new GetConsultationsByPracticeQuery { PracticeId = Practiceid, }); if (!result.Success) throw new Exception(result.Error ?? "");

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
        [HttpGet("get-consultation-by-id")]
        public async Task<IActionResult> GetConsultation([FromQuery] string Consultationmid)
        {
            try
            {
                var result = await _mediator.Send(new GetConsultationByIdQuery { ConsultationId = Consultationmid, }); if (!result.Success) throw new Exception(result.Error ?? "");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(Result<string>.Fail(ex.Message));
            }
        }
        [HttpPost("web-create-Practice-patient")]
        public async Task<IActionResult> WebCreatePracticePatient([FromBody] WebCreatePatientPracticeCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
        [HttpPost("create-Practice-patient")]
        public async Task<IActionResult> CreatePracticePatient([FromBody] CreatePracticePatientCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
        [HttpPost("Update-Practice-patient")]
        public async Task<IActionResult> UpdatePracticePatient([FromBody] UpdatePatientHouseholdCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
        [HttpDelete("Delete-Practice-patient")]
        public async Task<IActionResult> DeletePracticePatient([FromBody] DeletePracticePatientCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
        [HttpGet("get-patient-by-id")]
        public async Task<IActionResult> GetPatientA([FromQuery] string practiceid,string patientid)
        {
            var result = await _mediator.Send(new GetPatientByIdQuery {  PatientId= patientid, PracticeId = practiceid,}); if (!result.Success) throw new Exception(result.Error ?? "");

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
        [HttpGet("get-patient-by-householdidandpatientid")]
        public async Task<IActionResult> GetPatientHousehold([FromQuery] string practiceid , string householdi)
        {
            var result = await _mediator.Send(new GetPatientsByHouseholdQuery { HouseholdId = householdi, PracticeId = practiceid}); if (!result.Success) throw new Exception(result.Error ?? "");

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
        [HttpPost("Move-Practice-patient")]
        public async Task<IActionResult> MovePracticePatient([FromBody] DeletePracticePatientCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
        [HttpPost("Add-to-household")]
        public async Task<IActionResult> AddPatientToHousehold([FromBody] AddPatientToHouseHoldCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
        [HttpPost("create-appointment")]
        public async Task<IActionResult> CreatePatientAppointment([FromBody] CreatePatientAppointmentCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
        [HttpPost("update-appointment")]
        public async Task<IActionResult> UpdatePatientAppointment([FromBody] UpdatePatientAppointmentCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
        [HttpPost("center-update-appointment")]
        public async Task<IActionResult> CenterUpdatePatientAppointment([FromBody] CenterUpdateAppointment command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
        [HttpDelete("delete-appointment")]
        public async Task<IActionResult> DeletePatientAppointment([FromBody] DeletePatientAppointmentCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
        [HttpGet("get-appointment-by-id")]
        public async Task<IActionResult> GetPatientAppointment([FromQuery] string appointmentid)
        {
            var result = await _mediator.Send(new GetAppointmentByIdQuery { AppointmentId = appointmentid, }); if (!result.Success) throw new Exception(result.Error ?? "");

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
        [HttpGet("getall-appointment-by-patientid")]
        public async Task<IActionResult> GetallPatientAppointment([FromQuery] string Patientid)
        {
            var result = await _mediator.Send(new GetAppointmentsByPatientQuery { PatientId = Patientid, }); if (!result.Success) throw new Exception(result.Error ?? "");

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
