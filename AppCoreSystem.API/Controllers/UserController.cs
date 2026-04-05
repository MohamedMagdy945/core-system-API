using Microsoft.AspNetCore.Mvc;
using AppCoreSystem.Application.Features.Identity.Auth.Login.Commands;
using AppCoreSystem.Application.Features.Identity.Auth.Register.Commands.CreateUser;

namespace AppCoreSystem.API.Controllers
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

        //[HttpPost("refresh")]
        //public async Task<IActionResult> Refresh([FromBody] RefreshTokenCommand command)
        //{
        //    await _authorizationService.AuthorizeAsync(User, book, requirement);
        //    var result = await _mediator.Send(command);
        //    return NewResult(result);
        //}

    }
}
