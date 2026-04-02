using UniversitySystem.Domain.Identity;

namespace UniversitySystem.Application.Interfaces
{
    public interface ITokenGenerator
    {
        string GenerateAccessToken(AppUser user, IEnumerable<string> roles);
        string GenerateRefreshToken();
        string HashToken(string token);
    }
}
