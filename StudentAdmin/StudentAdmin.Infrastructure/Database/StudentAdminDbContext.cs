using System.Reflection;
using StudentAdmin.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace StudentAdmin.Infrastructure.Database;

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