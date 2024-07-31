using MediatR;
using StudentAdmin.Application.Exceptions;
using StudentAdmin.Core.Repositories;

namespace StudentAdmin.Application.Commands.UpdateStudent;

public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand>
{
    private readonly IStudentRepository _repository;

    public UpdateStudentCommandHandler(IStudentRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
    {
        var student = await _repository.GetByIdAsync(request.Id);
        if (student is null)
        {
            throw new NotFoundStudentException();
        }

        var fullName = string.Concat(request.Model.FirstName, " ", request.Model.LastName);
        student.Update(fullName, request.Model.Email, request.Model.IsActive);

        await _repository.UpdateAsync(student);
    }
}