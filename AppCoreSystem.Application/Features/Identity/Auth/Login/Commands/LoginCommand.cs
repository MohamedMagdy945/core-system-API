using AppCoreSystem.Application.Common.Bases;
using AppCoreSystem.Application.Common.Models;
using AppCoreSystem.Application.Interfaces.Auth;
using MediatR;

namespace AppCoreSystem.Application.Features.Identity.Auth.Login.Commands
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
                request.Password, request.Ip ?? "", request.Device ?? "");

            if (!result.IsSuccess)
                return ResponseHandler.Unauthorized<TokenResponse>(result.Error ?? "Invalid User Name Or Password");

            return ResponseHandler.Success(result.Data!, "Login successful");
        }

    }
}
