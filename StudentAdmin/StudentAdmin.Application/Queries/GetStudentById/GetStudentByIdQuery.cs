using MediatR;
using StudentAdmin.Application.Models.ViewModels;

namespace StudentAdmin.Application.Queries.GetStudentById;

public class GetStudentByIdQuery : IRequest<StudentDetailsViewModel>
{
    public string Ra { get; private set; }

    public GetStudentByIdQuery(string ra)
    {
        Ra = ra;
    }
}