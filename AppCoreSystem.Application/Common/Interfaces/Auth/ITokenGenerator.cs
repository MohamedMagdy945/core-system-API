using AppCoreSystem.Application.Common.Models;
using AppCoreSystem.Domain.Entities.Identity;

namespace AppCoreSystem.Application.Common.Interfaces.Auth
{
    public interface ITokenGenerator
    {
        TokenPair GenerateTokenPair(AppUser user, IEnumerable<string> roles, string ip, string device);
        string GenerateAccessToken(AppUser user, IEnumerable<string> roles);
        string GenerateRefreshToken();
        string HashToken(string token);
    }
}
