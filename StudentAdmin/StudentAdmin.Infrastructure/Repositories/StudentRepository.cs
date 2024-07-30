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

    public async Task<Student?> GetById(int id)
    {
        var student = await _context.Students.FindAsync(id);
        return student;

    }

    public async Task<List<Student>> GetAll()
    {
        var students = await _context.Students.ToListAsync();
        return students;
    }

    public async Task<Student> Create(Student student)
    {
        var result = _context.Students.Add(student);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public Task Update(Student student)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Student student)
    {
        throw new NotImplementedException();
    }
}