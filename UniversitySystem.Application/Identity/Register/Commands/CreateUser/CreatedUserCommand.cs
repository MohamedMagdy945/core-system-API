using MediatR;
using UniversitySystem.Application.Common.Bases;
using UniversitySystem.Application.Common.Models;
using UniversitySystem.Application.Interfaces;

namespace UniversitySystem.Application.Identity.Register.Commands.CreateUser
{
    public record CreateUserCommand(string UserName, string Email, string Password,
        string ConfirmPassword, string? Ip, string? Device) : IRequest<Response<TokenResponse>>;

    public class CreatedUserCommand : IRequestHandler<CreateUserCommand, Response<TokenResponse>>
    {
        private readonly IAuthService _authService;
        public CreatedUserCommand(IAuthService authService)
        {
            _authService = authService;
        }
        public async Task<Response<TokenResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {

            var result = await _authService.RegisterAsync(
                request.UserName,
                request.Email,
                request.Password,
                request.Ip,
                request.Device);

            return ResponseHandler.Success(result, "User registered successfully");
        }
    }
}
