using Microsoft.AspNetCore.Authorization;
using UniverstySystem.Infrastructure.Identity.Requirement;

namespace UniverstySystem.Infrastructure.Identity.Handler
{
    public class PermissionHandler
      : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {
            var userPermissions = context.User
                .FindAll("permission")
                .Select(x => x.Value);

            if (requirement.Permissions.Any(p => userPermissions.Contains(p)))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
