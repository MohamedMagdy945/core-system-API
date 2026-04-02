using UniversitySystem.Domain.Identity;

namespace UniversitySystem.Application.Interfaces
{
    public interface IUserService
    {
        Task<AppUser?> GetByIdAsync(int userId);
        Task<IEnumerable<AppUser>> GetAllAsync();

        Task<bool> CreateAsync(string userName, string email, string password);

        Task<bool> UpdateAsync(AppUser user);

        Task<bool> DeleteAsync(int userId);
    }
}
