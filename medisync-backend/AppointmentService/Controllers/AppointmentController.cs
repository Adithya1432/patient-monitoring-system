using AppointmentService.DTOs;
using AppointmentService.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _service;

        public AppointmentController(IAppointmentService service)
        {
            _service = service;
        }

        [HttpPost("bookAppointment")]
        public async Task<ActionResult<BookAppointmentResponseDto>> Book(
        BookAppointmentRequestDto request)
        {
            return Ok(await _service.BookAsync(request));
        }
    }
}
