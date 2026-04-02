using MediatR;
using UniversitySystem.Application.Common.Bases;
using UniversitySystem.Application.Common.Models;
using UniversitySystem.Application.Interfaces;

namespace UniversitySystem.Application.Identity.RefreshToeken.Commands
{
    public record RefreshTokenCommand(string RefreshToken) : IRequest<Response<TokenResponse>>;
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, Response<TokenResponse>>
    {
        private readonly IAuthService _authService;
        public RefreshTokenHandler(IAuthService authService)
        {
            _authService = authService;
        }
        public async Task<Response<TokenResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var result = await _authService.RefreshTokenAsync(request.RefreshToken, "");
            if (result == null)
                return ResponseHandler.Unauthorized<TokenResponse>("Invalid refresh token");
            return ResponseHandler.Success(result, "Token refreshed successfully");
        }
    }
}
