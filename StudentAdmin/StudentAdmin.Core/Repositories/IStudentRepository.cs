using StudentAdmin.Core.Entities;

namespace StudentAdmin.Core.Repositories;

public interface IStudentRepository
{
    Task<Student?> GetById(string id);
    Task<List<Student>> GetAll(string? query);
    Task<Student> Create(Student student);
    Task Update(Student student);
    Task Delete(Student student);
}