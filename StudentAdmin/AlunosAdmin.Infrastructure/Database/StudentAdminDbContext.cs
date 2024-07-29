using System.Reflection;
using AlunosAdmin.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AlunosAdmin.Infrastructure.Database;

public class StudentAdminDbContext : DbContext
{
    public StudentAdminDbContext(DbContextOptions<StudentAdminDbContext> options) : base(options) {}
    
    public DbSet<Student> Students { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}