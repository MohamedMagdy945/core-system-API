using Microsoft.AspNetCore.Identity;
using UniversitySystem.Application.Interfaces.Identity;
using UniversitySystem.Domain.Entities.Identity;

namespace UniverstySystem.Infrastructure.Services.Identity
{
    public class PermissionService : IPermissionService
    {
        private readonly UserManager<AppUser> _userManager;

        public PermissionService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IReadOnlyList<string>> GetUserPermissionsAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new List<string>();

            var roles = await _userManager.GetRolesAsync(user);

            var permissions = new List<string>();

            foreach (var roleName in roles)
            {
                var roleClaims = await _userManager.GetClaimsAsync(user);

                permissions.AddRange(
                    roleClaims
                        .Where(c => c.Type == "permission")
                        .Select(c => c.Value)
                );
            }

            return permissions.Distinct().ToList();
        }

        public async Task<bool> HasPermissionAsync(string userId, string permission)
        {
            var permissions = await GetUserPermissionsAsync(userId);
            return permissions.Contains(permission);
        }
    }
}
