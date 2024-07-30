using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudentAdmin.Application.Commands.CreateStudent;
using StudentAdmin.Application.Models.InputModels;
using StudentAdmin.Application.Queries.GetAllStudents;
using StudentAdmin.Application.Queries.GetStudentById;

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
        return CreatedAtAction(nameof(GetById), new { id = studentId }, data);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var query = new GetStudentByIdQuery(id);
        var student = await _mediator.Send(query);
        return Ok(student);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? search)
    {
        var query = new GetAllStudentsQuery(search);
        var students = await _mediator.Send(query);
        return Ok(students);
    }
}