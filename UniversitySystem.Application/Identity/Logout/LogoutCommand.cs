using MediatR;
using UniversitySystem.Application.Common.Bases;
using UniversitySystem.Application.Interfaces;

namespace UniversitySystem.Application.Identity.Logout
{
    public class LogOutCommand
    {
        public record LogoutCommand(string RefreshToken) : IRequest<Response<bool>>;
        public class LogoutHandler : IRequestHandler<LogoutCommand, Response<bool>>
        {
            private readonly IAuthService _authService;
            public LogoutHandler(IAuthService authService)
            {
                _authService = authService;
            }

            public async Task<Response<bool>> Handle(LogoutCommand request, CancellationToken cancellationToken)
            {
                var result = await _authService.LogoutAsync(request.RefreshToken);

                return ResponseHandler.Success(result, "Logged out successfully");
            }


        }
    }
}
