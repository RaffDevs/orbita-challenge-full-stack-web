using StudentAdmin.Core.Entities;

namespace StudentAdmin.Core.Repositories;

public interface IStudentRepository
{
    Task<Student?> GetByIdAsync(string id);
    Task<List<Student>> GetAllAsync(string? query);
    Task<Student> CreateAsync(Student student);
    Task UpdateAsync(Student student);
    Task DeleteAsync(Student student);
}