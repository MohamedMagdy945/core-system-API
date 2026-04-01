using Microsoft.EntityFrameworkCore;
using UniversitySystem.Domain.Entities;

namespace UniversitySystem.Application.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<AppUser> Users { get; }
        DbSet<Student> Students { get; }
        DbSet<Department> Departments { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
