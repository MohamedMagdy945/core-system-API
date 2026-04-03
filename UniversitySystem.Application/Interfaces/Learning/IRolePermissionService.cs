using UniversitySystem.Application.Common.Bases;
using UniversitySystem.Application.Common.DTOs;
using UniversitySystem.Application.Common.Wrappers;

namespace UniversitySystem.Application.Interfaces.Learning
{
    public interface IRolePermissionService
    {
        Task<Result> AssignPermissionToRoleAsync(string roleId, int permissionId);
        Task<Result<List<PermissionDTO>>> GetUserPermissionsAsync(int userId);
    }
}
