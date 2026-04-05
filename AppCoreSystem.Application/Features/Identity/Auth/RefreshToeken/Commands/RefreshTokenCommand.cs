using AppCoreSystem.Application.Common.Bases;
using AppCoreSystem.Application.Common.Models;
using AppCoreSystem.Application.Interfaces.Auth;
using MediatR;

namespace AppCoreSystem.Application.Features.Identity.Auth.RefreshToeken.Commands
{
    public record RefreshTokenCommand(string RefreshToken, string Ip) : IRequest<Response<TokenResponse>>;
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, Response<TokenResponse>>
    {
        private readonly IAuthService _authService;
        public RefreshTokenHandler(IAuthService authService)
        {
            _authService = authService;
        }
        public async Task<Response<TokenResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var ipAddress = request.Ip ?? string.Empty;

            var result = await _authService.RefreshTokenAsync(request.RefreshToken, ipAddress);

            if (!result.IsSuccess)
                return ResponseHandler.Unauthorized<TokenResponse>(result.Error!);

            return ResponseHandler.Success(result.Data!, "Token refreshed successfully");
        }
    }
}
