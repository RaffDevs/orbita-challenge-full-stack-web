using MediatR;
using StudentAdmin.Application.Models.ViewModels;
using StudentAdmin.Core.Entities;

namespace StudentAdmin.Application.Queries.GetAllStudents;

public class GetAllStudentsQuery : IRequest<List<StudentViewModel>>
{
    public string? Query { get; private set; }

    public GetAllStudentsQuery(string query)
    {
        Query = query.ToLower();
    }
}