using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using UniversitySystem.Application.Interfaces;
using UniversitySystem.Domain.Identity;
using UniverstySystem.Infrastructure.Models;

namespace UniverstySystem.Infrastructure.Service
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserClaimsPrincipalFactory<AppUser> _claimsFactory;
        public IdentityService(
            UserManager<AppUser> userManager, JwtSettings jwtSettings,
            IAuthorizationService authorizationService,
            IUserClaimsPrincipalFactory<AppUser> claimsFactory)
        {
            _userManager = userManager;
            _authorizationService = authorizationService;
            _claimsFactory = claimsFactory;
        }
        public async Task<AppUser?> GetUserByIdAsync(int userId)
        {
            return await _userManager.FindByIdAsync(userId.ToString());
        }
        public async Task<string?> GetUserNameAsync(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            return user?.UserName;
        }

        public async Task<(IdentityResult Result, int? UserId)> CreateUserAsync(string userName, string email, string password)
        {
            var user = new AppUser
            {
                UserName = userName,
                Email = email,
            };

            var result = await _userManager.CreateAsync(user, password);

            return (result, result.Succeeded ? user.Id : null);
        }
        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
                return false;

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }


        public async Task<bool> IsInRoleAsync(int userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
                return false;

            return await _userManager.IsInRoleAsync(user, role);
        }

        public async Task<bool> AuthorizeAsync(int userId, string policyName)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
                return false;

            ClaimsPrincipal principal = await _claimsFactory.CreateAsync(user);
            var result = await _authorizationService.AuthorizeAsync(principal, resource: null, policyName);
            return result.Succeeded;
        }

        public async Task<AppUser?> LoginAsync(string userName, string password)
        {
            var user = await _userManager.FindByEmailAsync(userName);

            if (user == null)
                user = await _userManager.FindByNameAsync(userName);

            if (user == null)
                return null;

            var isValid = await _userManager.CheckPasswordAsync(user, password);

            return isValid ? user : null;
        }
        public async Task<IEnumerable<string>> GetRolesAsync(AppUser appUser)
        {
            return await _userManager.GetRolesAsync(appUser);
        }
    }
}
