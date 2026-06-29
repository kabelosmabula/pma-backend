using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace PMA.MS.Patient.API.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class PracticeController : ControllerBase
    {
        //public PracticeController(CreatePracticeCommandHandler _handler) 
        //{
        
        //}

        //[HttpPost]
        //public async Task<IActionResult> Add(CreateProductCommand command)
        //{
        //    await _handler.Handle(command);
        //    return Ok();
        //}
    }
}
