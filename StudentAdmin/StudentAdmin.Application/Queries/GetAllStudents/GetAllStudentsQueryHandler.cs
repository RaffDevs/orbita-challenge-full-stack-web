using MediatR;
using StudentAdmin.Application.Models.Mappers;
using StudentAdmin.Application.Models.ViewModels;
using StudentAdmin.Core.Entities;
using StudentAdmin.Core.Repositories;

namespace StudentAdmin.Application.Queries.GetAllStudents;

public class GetAllStudentsQueryHandler : IRequestHandler<GetAllStudentsQuery, List<StudentViewModel>>
{
    private readonly IStudentRepository _repository;

    public GetAllStudentsQueryHandler(IStudentRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<StudentViewModel>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
    {
        var students = await _repository.GetAllAsync(request.Query);
        var studentsViewModel = students
            .Select(StudentMapper.MapToStudentViewModel)
            .ToList();
        return studentsViewModel;
    }
}