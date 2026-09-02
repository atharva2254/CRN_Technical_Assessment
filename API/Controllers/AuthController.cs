using CRN_Technical_Assessment.Application.DTOs;
using CRN_Technical_Assessment.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRN_Technical_Assessment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService service) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto request)
        { 
            var message = await service.LoginUser(request);

            if(message is null || !ModelState.IsValid)
            {
                return BadRequest("Invalid credentials");
            }

            return Ok(message);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto request)
        {
            var result = await service.RegisterUser(request);

            if(result == null)
            {
                return BadRequest("Something went wrong");
            }

            return Created();
        }
    }
}
