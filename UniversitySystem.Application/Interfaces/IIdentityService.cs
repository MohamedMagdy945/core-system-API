
using Microsoft.AspNetCore.Identity;

namespace UniversitySystem.Application.Interfaces
{
    public interface IIdentityService
    {
        Task<string?> GetUserNameAsync(int userId);

        Task<(IdentityResult Result, string? UserId)> CreateUserAsync(string userName, string email, string password);

        Task<bool> DeleteUserAsync(int userId);

        Task<bool> IsInRoleAsync(int userId, string role);

        Task<bool> AuthorizeAsync(int userId, string policyName);
    }
}
