using StudentAdmin.Application.Models.InputModels;
using StudentAdmin.Application.Models.ViewModels;
using StudentAdmin.Core.Entities;

namespace StudentAdmin.Application.Models.Mappers;

public static class StudentMapper
{
    public static Student MapToStudent(CreateStudentInputModel model)
    {
        var fullName = string.Concat(model.FirstName, " ", model.LastName);
        return new Student(
            model.Ra,
            fullName,
            model.Email,
            model.Cpf
        );
    }

    public static StudentViewModel MapToStudentViewModel(Student data)
    {
        return new StudentViewModel(
            data.FullName,
            data.Ra
        );
    }

    public static StudentDetailsViewModel MapToStudentDetailViewModel(Student data)
    {
        return new StudentDetailsViewModel(
            data.Ra,
            data.FullName,
            data.Email,
            data.Cpf
        );
    }
}