using AppCoreSystem.Application.Common.Bases;
using AppCoreSystem.Application.Common.Wrappers;
using AppCoreSystem.Application.Features.Identity.Permissions.Models;

namespace AppCoreSystem.Application.Interfaces.Learning
{
    public interface IRolePermissionService
    {
        Task<Result> AssignPermissionToRoleAsync(string roleId, int permissionId);
        Task<Result<List<PermissionDTO>>> GetUserPermissionsAsync(int userId);
    }
}
