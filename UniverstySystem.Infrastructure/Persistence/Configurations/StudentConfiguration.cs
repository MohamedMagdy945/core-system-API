using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniversitySystem.Domain.Entities.Business;

namespace UniverstySystem.Infrastructure.Persistence.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(s => s.Name)
                .IsUnique();

            builder.Property(s => s.PersonalEmail)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(s => s.PersonalEmail)
                .IsUnique();

            builder.HasOne(s => s.Department)
                .WithMany(d => d.Students)
                .HasForeignKey(s => s.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
