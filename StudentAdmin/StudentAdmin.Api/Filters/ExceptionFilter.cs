using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StudentAdmin.Application.Exceptions;

namespace StudentAdmin.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var result = new ObjectResult(context.Exception.Message);

        result.StatusCode = context.Exception switch
        {
            NotFoundStudentException => StatusCodes.Status404NotFound,
            UnexpectedExpcetion => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError
        };

        context.Result = result;
    }
}