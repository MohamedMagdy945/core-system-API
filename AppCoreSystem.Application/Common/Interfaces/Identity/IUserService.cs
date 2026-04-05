using Microsoft.AspNetCore.Identity;
using AppCoreSystem.Domain.Entities.Identity;

namespace AppCoreSystem.Application.Common.Interfaces.Identity
{
    public interface IUserService
    {
        Task<AppUser?> GetByIdAsync(int userId);

        Task<AppUser?> CheckPasswordAsync(string username, string password);
        Task<IEnumerable<AppUser>> GetAllAsync();

        Task<(IdentityResult Result, AppUser? User)> CreateAsync(string userName, string email, string password);

        Task<IdentityResult> UpdateAsync(int id, string userName, string email);

        Task<IdentityResult> DeleteAsync(int userId);

        Task<IdentityResult> AddRoleAsync(int userId, string roleName);

        Task<IdentityResult> RemoveRoleAsync(int userId, string roleName);

        Task<IList<string>> GetRolesAsync(int userId);
    }
}
