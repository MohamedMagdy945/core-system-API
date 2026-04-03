using UniversitySystem.Application.Common.Models;
using UniversitySystem.Application.Common.Wrappers;

namespace UniversitySystem.Application.Interfaces.Auth
{
    public interface IAuthService
    {
        Task<Result<TokenResponse>> RegisterAsync(string username, string email, string password, string ip, string device);
        Task<Result<TokenResponse>> LoginAsync(string username, string password, string ip, string device);
        Task<Result<TokenResponse>> RefreshTokenAsync(string refreshToken, string ip);
        Task<Result<bool>> LogoutAsync(string refreshToken);
    }
}
