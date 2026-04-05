using Microsoft.AspNetCore.Authorization;

namespace UniverstySystem.Infrastructure.Identity.Attribute
{
    public class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(params string[] permissions)
        {
            Policy = string.Join(",", permissions);
        }
    }
}
