using AppCoreSystem.Application.Common.Bases;
using AppCoreSystem.Application.Common.Models;

namespace AppCoreSystem.Application.Interfaces.Auth
{
    public interface IAuthService
    {
        Task<Result<TokenResponse>> RegisterAsync(string username, string email, string password, string ip, string device);
        Task<Result<TokenResponse>> LoginAsync(string username, string password, string ip, string device);
        Task<Result<TokenResponse>> RefreshTokenAsync(string refreshToken, string ip);
        Task<Result<bool>> LogoutAsync(string refreshToken);
    }
}
