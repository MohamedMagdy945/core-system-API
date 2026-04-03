using Microsoft.EntityFrameworkCore;
using UniversitySystem.Domain.Entities.Business;
using UniversitySystem.Domain.Entities.Identity;

namespace UniversitySystem.Application.Interfaces
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
