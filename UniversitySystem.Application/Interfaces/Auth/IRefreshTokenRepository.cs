using UniversitySystem.Application.Common.Wrappers;
using UniversitySystem.Domain.Entities.Identity;

namespace UniversitySystem.Application.Interfaces.Auth
{
    public interface IRefreshTokenRepository
    {
        Task<Result<bool>> AddAsync(RefreshToken token);
        Task<Result<RefreshToken>> GetByHashAsync(string tokenHash);
        Task<Result<bool>> UpdateAsync(RefreshToken token);
        Task<Result<bool>> RevokeAsync(string tokenHash);
        Task<Result<int>> RevokeAllForUserAsync(int userId);
    }
}
