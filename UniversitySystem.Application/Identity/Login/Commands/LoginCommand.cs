using MediatR;
using UniversitySystem.Application.Common.Bases;
using UniversitySystem.Application.Common.Models;
using UniversitySystem.Application.Interfaces;

namespace UniversitySystem.Application.Identity.Login.Commands
{
    public record LoginCommand(string UserName, string Password, string? Ip, string? Device) : IRequest<Response<TokenResponse>>;

    public class LoginHandler : IRequestHandler<LoginCommand, Response<TokenResponse>>
    {
        private readonly IAuthService _authService;
        public LoginHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<Response<TokenResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var result = await _authService.LoginAsync(request.UserName,
                request.Password, request.Ip!, request.Device!);

            if (result == null)
                return ResponseHandler.Unauthorized<TokenResponse>("Invalid email or password");

            return ResponseHandler.Success(result, "Login successful");
        }

    }
}
