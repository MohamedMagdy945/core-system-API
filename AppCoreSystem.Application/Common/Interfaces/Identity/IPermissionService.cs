namespace AppCoreSystem.Application.Common.Interfaces.Identity
{
    public interface IPermissionService
    {
        Task<IReadOnlyList<string>> GetUserPermissionsAsync(string userId);

        Task<bool> HasPermissionAsync(string userId, string permission);

    }
}
