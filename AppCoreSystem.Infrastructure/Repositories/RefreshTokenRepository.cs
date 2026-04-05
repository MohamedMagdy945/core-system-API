using AppCoreSystem.Application.Common.Bases;
using AppCoreSystem.Application.Interfaces.Auth;
using AppCoreSystem.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using UniverstySystem.Infrastructure.Persistence;

namespace UniverstySystem.Infrastructure.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AppDbContext _context;
        public RefreshTokenRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Result<bool>> AddAsync(RefreshToken token)
        {
            await _context.RefreshTokens.AddAsync(token);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
                return Result<bool>.Success(true);

            return Result<bool>.Failure("Failed to save the token.");
        }

        public async Task<Result<RefreshToken>> GetByHashAsync(string tokenHash)
        {
            var refreshToken = await _context.RefreshTokens
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.TokenHash == tokenHash);

            if (refreshToken == null)
                return Result<RefreshToken>.Failure("Token not found.");

            return Result<RefreshToken>.Success(refreshToken);

        }
        public async Task<Result<bool>> UpdateAsync(RefreshToken token)
        {
            _context.RefreshTokens.Update(token);

            var affectedRows = await _context.SaveChangesAsync();

            if (affectedRows > 0)
                return Result<bool>.Success(true);

            return Result<bool>.Failure("Token not found or no changes made.");
        }
        public async Task<Result<bool>> RevokeAsync(string tokenHash)
        {

            var token = await _context.RefreshTokens
                .FirstOrDefaultAsync(x => x.TokenHash == tokenHash && x.RevokedAt == null);

            if (token == null)
                return Result<bool>.Failure("Token not found or already revoked.");

            token.RevokedAt = DateTime.UtcNow;
            token.RevokedReason = "User logged out";

            await _context.SaveChangesAsync();

            return Result<bool>.Success(true);

        }
        public async Task<Result<int>> RevokeAllForUserAsync(int userId)
        {
            var affectedRows = await _context.RefreshTokens
                .Where(x => x.UserId == userId && x.RevokedAt == null)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(t => t.RevokedAt, DateTime.UtcNow)
                    .SetProperty(t => t.RevokedReason, "Logout all devices"));

            return Result<int>.Success(affectedRows);
        }


    }
}
