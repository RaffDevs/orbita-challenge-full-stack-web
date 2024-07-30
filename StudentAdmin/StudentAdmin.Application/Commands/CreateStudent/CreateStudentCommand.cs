using MediatR;
using StudentAdmin.Application.Models.InputModels;
using StudentAdmin.Application.Models.ViewModels;
using StudentAdmin.Core.Entities;

namespace StudentAdmin.Application.Commands.CreateStudent;

public class CreateStudentCommand : IRequest<string>
{
    public readonly CreateStudentInputModel Model;

    public CreateStudentCommand(CreateStudentInputModel model)
    {
        Model = model;
    }
}