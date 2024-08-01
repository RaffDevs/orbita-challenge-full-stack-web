using MediatR;
using StudentAdmin.Application.Models.Mappers;
using StudentAdmin.Application.Models.ViewModels;
using StudentAdmin.Core.Repositories;

namespace StudentAdmin.Application.Commands.CreateStudent;

public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, string>
{
    private readonly IStudentRepository _repository;

    public CreateStudentCommandHandler(IStudentRepository repository)
    {
        _repository = repository;
    }

    public async Task<string> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        var student = StudentMapper.MapToStudent(request.Model);
        var result = await _repository.CreateAsync(student);

        return result.Ra;
    }
}