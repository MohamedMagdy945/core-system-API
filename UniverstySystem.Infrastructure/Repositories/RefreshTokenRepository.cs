using Microsoft.EntityFrameworkCore;
using UniversitySystem.Application.Interfaces;
using UniversitySystem.Domain.Identity;
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

        public async Task AddAsync(RefreshToken token)
        {
            await _context.RefreshTokens.AddAsync(token);
            await _context.SaveChangesAsync();
        }

        public async Task<RefreshToken?> GetByHashAsync(string tokenHash)
        {
            return await _context.RefreshTokens
                       .FirstOrDefaultAsync(x => x.TokenHash == tokenHash);
        }
        public async Task UpdateAsync(RefreshToken token)
        {
            _context.RefreshTokens.Update(token);
            await _context.SaveChangesAsync();
        }

        public async Task RevokeAllForUserAsync(int userId)
        {

            var tokens = await _context.RefreshTokens
            .Where(x => x.UserId == userId && x.RevokedAt == null)
            .ToListAsync();

            foreach (var token in tokens)
            {
                token.RevokedAt = DateTime.UtcNow;
                token.RevokedReason = "Logout all devices";
            }

            await _context.SaveChangesAsync();
        }


    }
}
