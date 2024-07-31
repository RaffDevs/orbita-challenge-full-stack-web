using StudentAdmin.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StudentAdmin.Infrastructure.Database.Configuration;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasKey(s => s.Ra);
        builder
            .HasIndex(o => o.Email)
            .IsUnique();
        builder
            .HasIndex(s => s.Cpf)
            .IsUnique();
    }
}