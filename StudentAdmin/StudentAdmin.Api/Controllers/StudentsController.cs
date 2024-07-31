using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudentAdmin.Application.Commands.CreateStudent;
using StudentAdmin.Application.Commands.UpdateStudent;
using StudentAdmin.Application.Exceptions;
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
    public async Task<IActionResult> Create([FromBody] CreateStudentInputModel data)
    {
        try
        {
            var command = new CreateStudentCommand(data);
            var studentId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = studentId }, data);
        }
        catch (Exception ex)
        {
            throw new UnexpectedExpcetion(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        try
        {
            var query = new GetStudentByIdQuery(id);
            var student = await _mediator.Send(query);
            return Ok(student);
        }
        catch (Exception ex) when (ex is not NotFoundStudentException)
        {
            throw new UnexpectedExpcetion(ex.Message);
        }
        
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? search)
    {
        try
        {
            var query = new GetAllStudentsQuery(search);
            var students = await _mediator.Send(query);
            return Ok(students);
        }
        catch (Exception ex)
        {
            throw new UnexpectedExpcetion(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateStudentInputModel data)
    {
        try
        {
            var command = new UpdateStudentCommand(id, data);
            await _mediator.Send(command);
            return NoContent();
        }
        catch (Exception ex) when (ex is not NotFoundStudentException)
        {
            throw new UnexpectedExpcetion(ex.Message);
        }
        
    }
}