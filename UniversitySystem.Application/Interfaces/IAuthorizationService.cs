namespace UniversitySystem.Application.Interfaces
{
    public interface IAuthorizationService
    {
        Task<bool> IsInRoleAsync(int userId, string role);

        Task<bool> AuthorizeAsync(int userId, string policy);

        Task<IEnumerable<string>> GetUserPermissionsAsync(int userId);
    }
}
