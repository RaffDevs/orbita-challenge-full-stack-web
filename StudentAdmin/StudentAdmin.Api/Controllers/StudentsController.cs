using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudentAdmin.Application.Commands.CreateStudent;
using StudentAdmin.Application.Models.InputModels;

namespace StudentAdmin.Api.Controllers;

[Route("api/students")]
public class StudentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public StudentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateStudentInputModel data)
    {
        var command = new CreateStudentCommand(data);
        var studentId = await _mediator.Send(command);
        return Ok(studentId);
    }
    
}