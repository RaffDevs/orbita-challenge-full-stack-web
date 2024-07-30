using StudentAdmin.Core.Entities;

namespace StudentAdmin.Core.Repositories;

public interface IStudentRepository
{
    Task<Student?> GetById(int id);
    Task<List<Student>> GetAll();
    Task<Student> Create(Student student);
    Task Update(Student student);
    Task Delete(Student student);
}