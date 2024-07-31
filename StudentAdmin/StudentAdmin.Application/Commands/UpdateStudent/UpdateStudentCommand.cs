using MediatR;
using StudentAdmin.Application.Models.InputModels;

namespace StudentAdmin.Application.Commands.UpdateStudent;

public class UpdateStudentCommand : IRequest
{
    public string Id { get; private set; }
    public UpdateStudentInputModel Model { get; private set; }

    public UpdateStudentCommand(string id, UpdateStudentInputModel model)
    {
        Id = id;
        Model = model;
    }
}