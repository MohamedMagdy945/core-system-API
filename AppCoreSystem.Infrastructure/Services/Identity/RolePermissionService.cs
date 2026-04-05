using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using AppCoreSystem.Application.Common.Interfaces.Identity;

namespace UniverstySystem.Infrastructure.Services.Identity
{
    public class RolePermissionService : IRolePermissionService
    {
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public RolePermissionService(RoleManager<IdentityRole<int>> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> AddPermissionToRoleAsync(string roleName, string permission)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
                return IdentityResult.Failed(new IdentityError { Description = "Role not found" });

            var result = await _roleManager.AddClaimAsync(role,
                new Claim("permission", permission));

            return result;
        }

        public async Task<IdentityResult> RemovePermissionFromRoleAsync(string roleName, string permission)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
                return IdentityResult.Failed(new IdentityError { Description = "Role not found" });

            var claims = await _roleManager.GetClaimsAsync(role);

            var claim = claims.FirstOrDefault(c =>
                c.Type == "permission" && c.Value == permission);

            if (claim == null)
                return IdentityResult.Success;

            return await _roleManager.RemoveClaimAsync(role, claim);
        }

        public async Task<IdentityResult> ReplaceRolePermissionsAsync(string roleName, List<string> permissions)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
                return IdentityResult.Failed(new IdentityError { Description = "Role not found" });

            var claims = await _roleManager.GetClaimsAsync(role);

            foreach (var claim in claims.Where(c => c.Type == "permission"))
            {
                await _roleManager.RemoveClaimAsync(role, claim);
            }

            foreach (var p in permissions.Distinct())
            {
                await _roleManager.AddClaimAsync(role, new Claim("permission", p));
            }

            return IdentityResult.Success;
        }

        public async Task<IReadOnlyList<string>> GetRolePermissionsAsync(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
                return new List<string>();

            var claims = await _roleManager.GetClaimsAsync(role);

            return claims
                .Where(c => c.Type == "permission")
                .Select(c => c.Value)
                .ToList();
        }


        public async Task<IdentityResult> AssignMultiplePermissionsAsync(string roleName, List<string> permissions)
        {
            foreach (var p in permissions)
            {
                await AddPermissionToRoleAsync(roleName, p);
            }

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> RemoveMultiplePermissionsAsync(string roleName, List<string> permissions)
        {
            foreach (var p in permissions)
            {
                await RemovePermissionFromRoleAsync(roleName, p);
            }

            return IdentityResult.Success;
        }

        public async Task<bool> RoleHasPermissionAsync(string roleName, string permission)
        {
            var permissions = await GetRolePermissionsAsync(roleName);

            var exists = permissions.Contains(permission);

            return exists;
        }
    }
}
