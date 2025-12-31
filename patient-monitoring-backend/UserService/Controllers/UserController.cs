using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.DTOs;
using UserService.Interfaces;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpPost("patinetSignup")]
        public async Task<IActionResult> PatientSignup(PatientSignupDto dto)
        {
            try
            {
                await _service.PatientSignupAsync(dto);
                return Ok(new { message = "Patient Signup successful" });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { error = ex.Message });
            }
        }

        [HttpPost("doctorSignup")]
        public async Task<IActionResult> DoctorSignup(DoctorSignupDto dto)
        {
            try
            {
                await _service.DoctorSignupAsync(dto);
                return Ok(new { message = "Doctor Signup successful" });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { error = ex.Message });
            }
        }


        [HttpPatch("{userId:guid}/activate")]
        public async Task<IActionResult> ActivateUser(Guid userId)
        {
            try
            {
                await _service.ActivateUserAsync(userId);
                return Ok(new { message = "Account activated" });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { error = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Unexpected error" });
            }
        }
    }
}