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
        private readonly ITokenGenerator _tokenGenerator;
        public AuthService(IIdentityService identityService,
            IRefreshTokenRepository refreshTokenRepository,
            ITokenGenerator tokenGenerator
            )
        {
            _identityService = identityService;
            _refreshTokenRepository = refreshTokenRepository;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<TokenResponse> LoginAsync(string username, string password, string ip, string device)
        {
            var user = await _identityService.LoginAsync(username, password);

            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            var roles = await _identityService.GetRolesAsync(user);

            var accessToken = _tokenGenerator.GenerateAccessToken(user, roles);
            var refreshToken = _tokenGenerator.GenerateRefreshToken();

            var hash = _tokenGenerator.HashToken(refreshToken);

            var refreshTokenObj = new RefreshToken()
            {
                TokenHash = hash,
                UserId = user.Id,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                CreatedByIp = ip,
                DeviceInfo = device
            };
            await _refreshTokenRepository.AddAsync(refreshTokenObj);
            return new TokenResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                AccessTokenExpiration = DateTime.UtcNow.AddMinutes(15)
            };
        }

        public async Task<bool> LogoutAsync(string refreshToken)
        {
            var hash = _tokenGenerator.HashToken(refreshToken);
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
            var hash = _tokenGenerator.HashToken(refreshToken);

            var stored = await _refreshTokenRepository.GetByHashAsync(hash);

            if (stored == null ||
                stored.RevokedAt != null ||
                stored.ExpiresAt < DateTime.UtcNow ||
                stored.IsUsed)
            {
                throw new UnauthorizedAccessException("Invalid refresh token");
            }
            stored.IsUsed = true;
            stored.RevokedAt = DateTime.UtcNow;
            stored.RevokedByIp = ip;

            var user = await _identityService.GetUserByIdAsync(stored.UserId);
            var roles = await _identityService.GetRolesAsync(user!);

            var newAccess = _tokenGenerator.GenerateAccessToken(user!, roles);

            var newRefresh = _tokenGenerator.GenerateRefreshToken();

            var newHash = _tokenGenerator.HashToken(newRefresh);

            await _refreshTokenRepository.UpdateAsync(stored);

            await _refreshTokenRepository.AddAsync(new RefreshToken
            {
                TokenHash = newHash,
                UserId = stored.UserId,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                CreatedByIp = ip
            });

            return new TokenResponse
            {
                AccessToken = newAccess,
                RefreshToken = newRefresh,
                AccessTokenExpiration = DateTime.UtcNow.AddMinutes(15)
            };

        }

        public async Task<TokenResponse> RegisterAsync(string username, string email, string password, string ip, string device)
        {
            var (result, userId) = await _identityService.CreateUserAsync(username, email, password);

            if (!result.Succeeded)
                throw new BadRequestAppException(string.Join(", ", result.Errors.Select(e => e.Description)));

            var user = await _identityService.GetUserByIdAsync(userId!.Value);

            var roles = await _identityService.GetRolesAsync(user);

            var accessToken = _tokenGenerator.GenerateAccessToken(user, roles);
            var refreshToken = _tokenGenerator.GenerateRefreshToken();

            var hash = _tokenGenerator.HashToken(refreshToken);

            await _refreshTokenRepository.AddAsync(new RefreshToken
            {
                TokenHash = hash,
                UserId = user.Id,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                CreatedByIp = ip,
                DeviceInfo = device
            });

            return new TokenResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                AccessTokenExpiration = DateTime.UtcNow.AddMinutes(15),
                RefreshTokenExpiration = DateTime.UtcNow.AddDays(7)
            };

        }
    }
}

