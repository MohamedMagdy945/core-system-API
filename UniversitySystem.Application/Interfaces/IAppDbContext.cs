using Microsoft.EntityFrameworkCore;
using UniversitySystem.Domain.Entities;
using UniversitySystem.Domain.Identity;

namespace UniversitySystem.Application.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<RefreshToken> RefreshTokens { get; }
        DbSet<AppUser> Users { get; }
        DbSet<Student> Students { get; }
        DbSet<Department> Departments { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
