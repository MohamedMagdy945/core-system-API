using Microsoft.AspNetCore.Identity;

namespace AppCoreSystem.Application.Interfaces.Identity
{
    public interface IRolePermissionService
    {
        Task<IdentityResult> AddPermissionToRoleAsync(string roleName, string permission);

        Task<IdentityResult> RemovePermissionFromRoleAsync(string roleName, string permission);

        Task<IdentityResult> ReplaceRolePermissionsAsync(string roleName, List<string> permissions);

        Task<IdentityResult> AssignMultiplePermissionsAsync(string roleName, List<string> permissions);

        Task<IdentityResult> RemoveMultiplePermissionsAsync(string roleName, List<string> permissions);

        Task<IReadOnlyList<string>> GetRolePermissionsAsync(string roleName);

        Task<bool> RoleHasPermissionAsync(string roleName, string permission);
    }
}
