namespace StudentAdmin.Application.Models.InputModels;

public record CreateStudentInputModel(
    string Ra,
    string FirstName,
    string LastName,
    string Email,
    string Cpf
);