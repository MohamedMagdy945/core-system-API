using Microsoft.AspNetCore.Authorization;

namespace UniverstySystem.Infrastructure.Identity.Requirement
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public string[] Permissions { get; }

        public PermissionRequirement(string[] permissions)
        {
            Permissions = permissions;
        }
    }
}
