using UniversitySystem.Domain.Identity;

namespace UniversitySystem.Application.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task AddAsync(RefreshToken token);
        Task<RefreshToken?> GetByHashAsync(string tokenHash);
        Task UpdateAsync(RefreshToken token);
        Task RevokeAllForUserAsync(int userId);
    }
}
