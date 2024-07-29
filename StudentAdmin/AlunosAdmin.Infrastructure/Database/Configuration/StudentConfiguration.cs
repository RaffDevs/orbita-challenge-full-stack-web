using AlunosAdmin.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlunosAdmin.Infrastructure.Database.Configuration;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasKey(s => s.Ra);
        builder
            .HasIndex(o => o.Email)
            .IsUnique();
    }
}