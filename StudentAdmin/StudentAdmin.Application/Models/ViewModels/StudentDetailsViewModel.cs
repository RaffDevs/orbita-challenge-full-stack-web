namespace StudentAdmin.Application.Models.ViewModels;

public record StudentDetailsViewModel(
    string Ra,
    string FullName,
    string Email, 
    string Cpf,
    bool IsActive
);