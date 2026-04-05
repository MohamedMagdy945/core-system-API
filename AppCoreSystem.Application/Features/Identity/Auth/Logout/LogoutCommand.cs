using AppCoreSystem.Application.Common.Bases;
using AppCoreSystem.Application.Interfaces.Auth;
using MediatR;

namespace AppCoreSystem.Application.Features.Identity.Auth.Logout
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

                if (!result.IsSuccess)
                {
                    return ResponseHandler.Unauthorized<bool>(result.Error!);
                }

                return ResponseHandler.Success(true, "Logged out successfully");
            }


        }
    }
}
