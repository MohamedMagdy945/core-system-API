using MediatR;
using UniversitySystem.Application.Common.Bases;
using UniversitySystem.Application.Identity.Login.Models;
using UniversitySystem.Application.Interfaces;

namespace UniversitySystem.Application.Identity.Login.Commands
{
    public record LoginCommand(string UserName, string Password) : IRequest<Response<AuthResponseDTO>>;

    public class LoginHandler : IRequestHandler<LoginCommand, Response<AuthResponseDTO>>
    {
        private readonly IIdentityService _identityService;
        private readonly IJwtTokenGenerator _jwtTokenGeneratorl;
        public LoginHandler(IIdentityService identityService, IJwtTokenGenerator jwtTokenGenerator)
        {
            _identityService = identityService;
            _jwtTokenGeneratorl = jwtTokenGenerator;
        }

        public async Task<Response<AuthResponseDTO>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _identityService.LoginAsync(request.UserName, request.Password);

            if (user == null) return ResponseHandler.Unauthorized<AuthResponseDTO>("Invalid email or password");

            var roles = await _identityService.GetRolesAsync(user);

            var token = _jwtTokenGeneratorl.GenerateToken(user, roles);

            var authResponse = new AuthResponseDTO(token, user.UserName, user.Email);

            return ResponseHandler.Success(authResponse, "Login successful");
        }
    }
}
