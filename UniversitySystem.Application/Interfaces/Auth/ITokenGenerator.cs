using UniversitySystem.Application.Common.Models;
using UniversitySystem.Domain.Entities.Identity;

namespace UniversitySystem.Application.Interfaces.Auth
{
    public interface ITokenGenerator
    {
        TokenPair GenerateTokenPair(AppUser user, IEnumerable<string> roles, string ip, string device);
        string GenerateAccessToken(AppUser user, IEnumerable<string> roles);
        string GenerateRefreshToken();
        string HashToken(string token);
    }
}
