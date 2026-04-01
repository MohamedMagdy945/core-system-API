using UniversitySystem.Domain.Entities;

namespace UniversitySystem.Application.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(AppUser user, IEnumerable<string> roles);
    }
}
