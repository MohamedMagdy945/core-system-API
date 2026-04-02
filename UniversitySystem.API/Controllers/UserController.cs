using Microsoft.AspNetCore.Mvc;
using UniversitySystem.Application.Identity.Login.Commands;
using UniversitySystem.Application.Identity.RefreshToeken.Commands;
using UniversitySystem.Application.Identity.Register.Commands.CreateUser;

namespace UniversitySystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : AppControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateUserCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginCommand command)
        {

            var response = await _mediator.Send(command);
            return NewResult(response);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

    }
}
