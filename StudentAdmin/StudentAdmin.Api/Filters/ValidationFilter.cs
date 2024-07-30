using Microsoft.AspNetCore.Mvc.Filters;

namespace StudentAdmin.Api.Filters;

public class ValidationFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var messages = context.ModelState
                .SelectMany(msg => msg.Value.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
        }
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}