namespace StudentAdmin.Application.Models.InputModels;

public record UpdateStudentInputModel(
    string FirstName,
    string LastName,
    string Email,
    bool IsActive
);