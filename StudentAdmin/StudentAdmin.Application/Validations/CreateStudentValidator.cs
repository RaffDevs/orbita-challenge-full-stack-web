using FluentValidation;
using StudentAdmin.Application.Models.InputModels;
using StudentAdmin.Core.Entities;
using StudentAdmin.Core.Exceptions;

namespace StudentAdmin.Application.Validations;

public class CreateStudentValidator : AbstractValidator<CreateStudentInputModel>
{
    public CreateStudentValidator()
    {
        RuleFor(p => p.FirstName)
            .NotNull()
            .WithMessage("O primeiro nome deve ser fornecido!")
            .NotEmpty()
            .WithMessage("O primeiro nome não pode estar vazio!")
            .MinimumLength(3)
            .WithMessage("O primeiro nome deve ter pelo menos 3 caracteres!")
            .MaximumLength(15)
            .WithMessage("O primeiro nome é muito longo. O máximo permitido é 15 caracteres!");

        RuleFor(p => p.LastName)
            .NotNull()
            .WithMessage("O sobrenome deve ser fornecido!")
            .NotEmpty()
            .WithMessage("O sobrenome não pode estar vazio!")
            .MinimumLength(3)
            .WithMessage("O sobrenome deve ter pelo menos 3 caracteres!")
            .MaximumLength(30)
            .WithMessage("O sobrenome é muito longo. O máximo permitido é 30 caracteres!");

        RuleFor(p => p.Email)
            .NotNull()
            .WithMessage("O e-mail deve ser fornecido!")
            .EmailAddress()
            .WithMessage("Por favor, insira um e-mail válido!");

        RuleFor(p => p.Ra)
            .NotNull()
            .WithMessage("O RA deve ser fornecido!")
            .NotEmpty()
            .WithMessage("O RA não pode estar vazio!")
            .MinimumLength(6)
            .WithMessage("O RA deve ter pelo menos 6 caracteres!")
            .MaximumLength(15)
            .WithMessage("O RA é muito longo. O máximo permitido é 15 caracteres!");

        RuleFor(p => p.Cpf)
            .NotNull()
            .WithMessage("O CPF deve ser fornecido!")
            .NotEmpty()
            .WithMessage("O CPF não pode estar vazio!")
            .Must(CheckCpf)
            .WithMessage("Insira um CPF válido!");
        
    }

    private bool CheckCpf(string cpf)
    {
        try
        {
            var doc = Student.ValidateCpf(cpf);
            return true;
        }
        catch (InvalidCpfException ex)
        {
            return false;
        }
    }
}