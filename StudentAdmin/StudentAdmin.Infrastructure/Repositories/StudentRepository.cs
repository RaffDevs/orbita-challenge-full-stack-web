using Microsoft.EntityFrameworkCore;
using StudentAdmin.Core.Entities;
using StudentAdmin.Core.Repositories;
using StudentAdmin.Infrastructure.Database;

namespace StudentAdmin.Infrastructure.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly StudentAdminDbContext _context;

    public StudentRepository(StudentAdminDbContext context)
    {
        _context = context;
    }

    public async Task<Student?> GetById(string id)
    {
        var student = await _context.Students.FindAsync(id);
        return student;
    }

    public async Task<List<Student>> GetAll(string? query)
    {
        IQueryable<Student> students = _context.Students;
        
        if (!string.IsNullOrEmpty(query))
        {
            students = students
                .Where(s =>
                    s.FullName.Contains(query) ||
                    s.Email.Contains(query) ||
                    s.Cpf.Contains(query) ||
                    s.Ra.Contains(query)
                );
        }

        return await students.ToListAsync();
    }

    public async Task<Student> Create(Student student)
    {
        var result = _context.Students.Add(student);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task Update(Student student)
    {
        _context.Students.Update(student);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Student student)
    {
        _context.Students.Remove(student);
        await _context.SaveChangesAsync();
    }
}