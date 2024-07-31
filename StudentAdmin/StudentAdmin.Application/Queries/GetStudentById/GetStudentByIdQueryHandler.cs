using MediatR;
using StudentAdmin.Application.Exceptions;
using StudentAdmin.Application.Models.Mappers;
using StudentAdmin.Application.Models.ViewModels;
using StudentAdmin.Core.Repositories;

namespace StudentAdmin.Application.Queries.GetStudentById;

public class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, StudentDetailsViewModel>
{
    private readonly IStudentRepository _repository;

    public GetStudentByIdQueryHandler(IStudentRepository repository)
    {
        _repository = repository;
    }

    public async Task<StudentDetailsViewModel> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
    {
        var student = await _repository.GetById(request.Ra);

        if (student is null)
        {
            throw new NotFoundStudentException();
        }

        var studentViewModelDetails = StudentMapper.MapToStudentDetailViewModel(student);
        return studentViewModelDetails;
    }
}