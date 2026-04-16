using AppCoreSystem.Application.Common.Bases;
using AppCoreSystem.Domain.Entities.Identity;

namespace AppCoreSystem.Application.Interfaces.Auth
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
