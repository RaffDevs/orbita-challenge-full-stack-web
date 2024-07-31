using MediatR;
using StudentAdmin.Application.Exceptions;
using StudentAdmin.Core.Repositories;

namespace StudentAdmin.Application.Commands.DeleteStudent;

public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand>
{
    private readonly IStudentRepository _repository;

    public DeleteStudentCommandHandler(IStudentRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
    {
        var student = await _repository.GetById(request.Id);

        if (student is null)
        {
            throw new NotFoundStudentException();
        }

        await _repository.Delete(student);
    }
}