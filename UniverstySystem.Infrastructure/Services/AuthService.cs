using UniversitySystem.Application.Common.Exceptions;
using UniversitySystem.Application.Common.Models;
using UniversitySystem.Application.Interfaces;
using UniversitySystem.Domain.Identity;
namespace UniverstySystem.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IIdentityService _identityService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly ITokenService _tokenService;
        public AuthService(IIdentityService identityService,
            IRefreshTokenRepository refreshTokenRepository,
            ITokenService tokenService
            )
        {
            _identityService = identityService;
            _refreshTokenRepository = refreshTokenRepository;
            _tokenService = tokenService;
        }
        public async Task<TokenResponse> RegisterAsync(string username, string email, string password, string ip, string device)
        {
            var (result, userId) = await _identityService.CreateUserAsync(username, email, password);

            if (!result.Succeeded)
                throw new BadRequestAppException(string.Join(", ", result.Errors.Select(e => e.Description)));

            var user = await _identityService.GetUserByIdAsync(userId!.Value);

            var roles = await _identityService.GetRolesAsync(user);

            var tokens = _tokenService.GenerateTokenPair(user, roles, ip, device);

            await SaveRefreshToken(user.Id, tokens);

            return Map(tokens);

        }

        public async Task<TokenResponse> LoginAsync(string username, string password, string ip, string device)
        {


            var user = await _identityService.LoginAsync(username, password);

            if (user == null)
                throw new UnauthorizedAccessException("Invalid credentials");

            var roles = await _identityService.GetRolesAsync(user);

            var tokens = _tokenService.GenerateTokenPair(user, roles, ip, device);

            await SaveRefreshToken(user.Id, tokens);

            return Map(tokens);
        }

        public async Task<bool> LogoutAsync(string refreshToken)
        {
            var hash = _tokenService.HashToken(refreshToken);

            var stored = await _refreshTokenRepository.GetByHashAsync(hash);

            if (stored == null)
                return false;

            stored.RevokedAt = DateTime.UtcNow;
            stored.RevokedReason = "Logout";

            await _refreshTokenRepository.UpdateAsync(stored);

            return true;

        }

        public async Task<TokenResponse> RefreshTokenAsync(string refreshToken, string ip)
        {
            var hash = _tokenService.HashToken(refreshToken);

            var stored = await _refreshTokenRepository.GetByHashAsync(hash);

            if (stored == null || stored.RevokedAt != null || stored.ExpiresAt < DateTime.UtcNow)
                throw new UnauthorizedAccessException("Invalid refresh token");

            stored.IsUsed = true;
            stored.RevokedAt = DateTime.UtcNow;
            stored.RevokedByIp = ip;

            await _refreshTokenRepository.UpdateAsync(stored);

            var user = await _identityService.GetUserByIdAsync(stored.UserId);
            var roles = await _identityService.GetRolesAsync(user!);

            var newTokens = _tokenService.GenerateTokenPair(user!, roles, ip, stored.DeviceInfo);

            await SaveRefreshToken(user!.Id, newTokens);

            return Map(newTokens);

        }
        private async Task SaveRefreshToken(int userId, TokenPair tokens)
        {
            await _refreshTokenRepository.AddAsync(new RefreshToken
            {
                TokenHash = tokens.RefreshTokenHash,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = tokens.RefreshTokenExpiration,
                CreatedByIp = tokens.Ip,
                DeviceInfo = tokens.Device
            });
        }
        private TokenResponse Map(TokenPair tokens)
        {
            return new TokenResponse
            {
                AccessToken = tokens.AccessToken,
                RefreshToken = tokens.RefreshToken,
                AccessTokenExpiration = tokens.AccessTokenExpiration,
                RefreshTokenExpiration = tokens.RefreshTokenExpiration
            };
        }


    }
}

