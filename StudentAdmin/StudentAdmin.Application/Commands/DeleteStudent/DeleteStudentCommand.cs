using MediatR;

namespace StudentAdmin.Application.Commands.DeleteStudent;

public class DeleteStudentCommand : IRequest
{
    public string Id { get; private set; }

    public DeleteStudentCommand(string id)
    {
        Id = id;
    }
}