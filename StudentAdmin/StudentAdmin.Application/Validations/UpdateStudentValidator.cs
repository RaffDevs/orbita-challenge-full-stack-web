using FluentValidation;
using StudentAdmin.Application.Models.InputModels;

namespace StudentAdmin.Application.Validations;

public class UpdateStudentValidator : AbstractValidator<UpdateStudentInputModel>
{
    public UpdateStudentValidator()
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
    }
}