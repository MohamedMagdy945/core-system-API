using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using UniversitySystem.Application.Common.Models;
using UniversitySystem.Application.Interfaces;
using UniversitySystem.Domain.Identity;
using UniverstySystem.Infrastructure.Models;

namespace UniverstySystem.Infrastructure.Service
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;

        public TokenService(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }

        public TokenPair GenerateTokenPair(AppUser user, IEnumerable<string> roles, string ip, string device)
        {
            var accessToken = GenerateAccessToken(user, roles);
            var refreshToken = GenerateRefreshToken();

            return new TokenPair
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                RefreshTokenHash = HashToken(refreshToken),
                AccessTokenExpiration = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpiryMinutes),
                RefreshTokenExpiration = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpiryDays),
                Ip = ip,
                Device = device
            };
        }

        public string GenerateAccessToken(AppUser user, IEnumerable<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId , user.UserName!),
                new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString())
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.AccessTokenSecret));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpiryMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var bytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);

            return Convert.ToBase64String(bytes);
        }
        public string HashToken(string token)
        {
            var key = Encoding.UTF8.GetBytes("@!#MasterJScetKe1/23Sdkjadshka123Nef789");

            using var hmac = new HMACSHA256(key);
            var bytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(token));

            return Convert.ToBase64String(bytes);
        }

    }
}
