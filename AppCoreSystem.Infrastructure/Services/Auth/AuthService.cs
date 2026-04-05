using AppCoreSystem.Application.Common.Bases;
using AppCoreSystem.Application.Common.Interfaces.Auth;
using AppCoreSystem.Application.Common.Interfaces.Identity;
using AppCoreSystem.Application.Common.Models;
using AppCoreSystem.Application.Interfaces.Auth;
using AppCoreSystem.Domain.Entities.Identity;
namespace UniverstySystem.Infrastructure.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly ITokenGenerator _tokenService;
        public AuthService(IUserService userService,
            IRefreshTokenRepository refreshTokenRepository,
            ITokenGenerator tokenService
            )
        {
            _userService = userService;
            _refreshTokenRepository = refreshTokenRepository;
            _tokenService = tokenService;
        }
        public async Task<Result<TokenResponse>> RegisterAsync(string username, string email, string password, string ip, string device)
        {
            var (result, user) = await _userService.CreateAsync(username, email, password);

            if (!result.Succeeded || user == null)
            {
                var error = result.Errors.Select(e => e.Description).FirstOrDefault() ?? "Authentication failed";
                return Result<TokenResponse>.Failure(error);

            }

            var roles = await _userService.GetRolesAsync(user.Id);

            var tokens = _tokenService.GenerateTokenPair(user, roles, ip, device);

            var saveResult = await SaveRefreshToken(user.Id, tokens);

            if (!saveResult.IsSuccess)
                return Result<TokenResponse>.Failure(saveResult.Error!);

            return Result<TokenResponse>.Success(MapToTokenResponse(tokens));
        }

        public async Task<Result<TokenResponse>> LoginAsync(string username, string password, string ip, string device)
        {

            var user = await _userService.CheckPasswordAsync(username, password);

            if (user == null)
                return Result<TokenResponse>.Failure("username or password is invalid");

            var roles = await _userService.GetRolesAsync(user.Id);

            var tokens = _tokenService.GenerateTokenPair(user, roles, ip, device);

            await SaveRefreshToken(user.Id, tokens);

            return Result<TokenResponse>.Success(MapToTokenResponse(tokens));
        }

        public async Task<Result<bool>> LogoutAsync(string refreshToken)
        {
            var hashToken = _tokenService.HashToken(refreshToken);
            var result = await _refreshTokenRepository.RevokeAsync(hashToken);

            if (result.IsSuccess)
                return Result<bool>.Success(true);

            return Result<bool>.Failure("Session already closed or invalid token.");

        }

        public async Task<Result<TokenResponse>> RefreshTokenAsync(string refreshToken, string ip)
        {
            var hashToken = _tokenService.HashToken(refreshToken);

            var storedResult = await _refreshTokenRepository.GetByHashAsync(hashToken);

            var storedHashToken = storedResult.Data;

            if (!storedResult.IsSuccess || storedHashToken == null || storedHashToken.RevokedAt != null
                || storedHashToken.ExpiresAt < DateTime.UtcNow)
            {
                return Result<TokenResponse>.Failure("Invalid session.");
            }

            storedHashToken.RevokedAt = DateTime.UtcNow;

            storedHashToken.RevokedReason = "Replaced by new token";

            storedHashToken.RevokedByIp = ip;

            await _refreshTokenRepository.UpdateAsync(storedHashToken);

            var user = await _userService.GetByIdAsync(storedHashToken.UserId);

            if (user == null)
                return Result<TokenResponse>.Failure("User not found.");

            var roles = await _userService.GetRolesAsync(user.Id);

            var newTokens = _tokenService.GenerateTokenPair(user, roles, ip, storedHashToken.DeviceInfo!);

            await SaveRefreshToken(user.Id, newTokens);

            return Result<TokenResponse>.Success(MapToTokenResponse(newTokens));

        }
        private async Task<Result<bool>> SaveRefreshToken(int userId, TokenPair tokens)
        {
            var result = await _refreshTokenRepository.AddAsync(new RefreshToken
            {
                TokenHash = tokens.RefreshTokenHash,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = tokens.RefreshTokenExpiration,
                CreatedByIp = tokens.Ip,
                DeviceInfo = tokens.Device
            });
            return result;
        }
        private TokenResponse MapToTokenResponse(TokenPair tokens)
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

