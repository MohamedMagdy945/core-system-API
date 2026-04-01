
using Microsoft.AspNetCore.Identity;
using UniversitySystem.Domain.Entities;

namespace UniversitySystem.Application.Interfaces
{
    public interface IIdentityService
    {
        Task<string?> GetUserNameAsync(int userId);
        Task<(IdentityResult Result, string? UserId)> CreateUserAsync(string userName, string email, string password);

        Task<AppUser?> LoginAsync(string userName, string password);

        Task<bool> DeleteUserAsync(int userId);

        Task<bool> IsInRoleAsync(int userId, string role);

        Task<bool> AuthorizeAsync(int userId, string policyName);

        Task<IEnumerable<string>> GetRolesAsync(AppUser user);
    }
}
