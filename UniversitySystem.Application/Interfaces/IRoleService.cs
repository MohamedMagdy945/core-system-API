namespace UniversitySystem.Application.Interfaces
{
    internal interface IRoleService
    {
        Task<bool> CreateRoleAsync(string roleName);

        Task<bool> DeleteRoleAsync(string roleName);

        Task<bool> AddUserToRoleAsync(int userId, string role);

        Task<bool> RemoveUserFromRoleAsync(int userId, string role);

        Task<IEnumerable<string>> GetUserRolesAsync(int userId);
    }
}
