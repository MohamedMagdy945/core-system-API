using UniversitySystem.Application.Common.Models;

namespace UniversitySystem.Application.Interfaces
{
    public interface IAuthService
    {
        Task<TokenResponse> RegisterAsync(string username, string email, string password, string ip, string device);
        Task<TokenResponse> LoginAsync(string username, string password, string ip, string device);
        Task<TokenResponse> RefreshTokenAsync(string refreshToken, string ip);
        Task<bool> LogoutAsync(string refreshToken);
    }
}
