
using Microsoft.AspNetCore.Identity;

namespace UniversitySystem.Domain.Entities.Identity
{
    public class RolePermission
    {
        public string RoleId { get; set; } = null!;
        public IdentityRole<int> Role { get; set; } = null!;

        public int PermissionId { get; set; }
        public Permission Permission { get; set; } = null!;
    }
}
