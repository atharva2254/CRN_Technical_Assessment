using Azure.Core;
using CRN_Technical_Assessment.Application.DTOs;
using CRN_Technical_Assessment.Application.Interfaces;
using CRN_Technical_Assessment.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRN_Technical_Assessment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService service) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await service.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await service.GetUserById(id);
            if(user is null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(UserDto request)
        {
            if(request.ConfirmPassword != request.Password)
            {
                return BadRequest("Password does not match");
            }

            var user = await service.CreateUser(request);
            if(user  == null)
            {
                return BadRequest("User already exists");
            }

            return Created();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, User request)
        {
            var user = await service.GetUserById(request.Id);

            if (id != request.Id || user == null)
            {
                return BadRequest();
            }

            await service.UpdateUser(request);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await service.DeleteUser(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
