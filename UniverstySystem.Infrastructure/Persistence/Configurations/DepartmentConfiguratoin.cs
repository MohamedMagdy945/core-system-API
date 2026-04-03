using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniversitySystem.Domain.Entities.Business;

namespace UniverstySystem.Infrastructure.Persistence.Configurations
{
    public class DepartmentConfiguratoin : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(d => d.Name)
               .IsRequired()
               .HasMaxLength(100);

            builder.Property(d => d.Code)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(d => d.Description)
                .HasMaxLength(500);

            builder.HasIndex(d => d.Code)
              .IsUnique();

        }
    }
}
