namespace UniversitySystem.Domain.Entities.Identity
{
    public class Permission
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public ICollection<RolePermission> RolePermissions { get; set; }
            = new List<RolePermission>();
    }
}
