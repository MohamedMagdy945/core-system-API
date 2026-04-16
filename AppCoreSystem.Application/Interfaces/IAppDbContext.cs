using Microsoft.EntityFrameworkCore;
using AppCoreSystem.Domain.Entities.Business;
using AppCoreSystem.Domain.Entities.Identity;

namespace AppCoreSystem.Application.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<RefreshToken> RefreshTokens { get; }
        DbSet<AppUser> Users { get; }
        DbSet<Student> Students { get; }
        DbSet<Department> Departments { get; }
        //DbSet<RolePermission> RolePermissions { get; }
        //DbSet<UserPermission> UserPermissions { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
