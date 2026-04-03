using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using UniverstySystem.Infrastructure.Identity.Requirement;

namespace UniverstySystem.Infrastructure.Identity.Provider
{
    public class PermissionPolicyProvider
      : DefaultAuthorizationPolicyProvider
    {
        public PermissionPolicyProvider(IOptions<AuthorizationOptions> options)
            : base(options) { }

        public override Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            var permissions = policyName.Split(',');

            var policy = new AuthorizationPolicyBuilder()
                .AddRequirements(new PermissionRequirement(permissions))
                .Build();

            return Task.FromResult<AuthorizationPolicy?>(policy);
        }
    }
}
