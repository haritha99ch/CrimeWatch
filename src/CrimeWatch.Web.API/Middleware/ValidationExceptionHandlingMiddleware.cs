using FluentValidation;
using System.Text.Json;

namespace CrimeWatch.Web.API.Middleware;
public class ValidationExceptionHandlingMiddleware : IMiddleware
{

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException e)
        {
            var problems = e.Errors.Select(error => new ProblemDetails
                {
                    Status = int.TryParse(error.ErrorCode, out var code) ? code : 400,
                    Title = "Validation Error",
                    Detail = error.ErrorMessage,
                    Type = "Server Error"

                }
            ).ToList();

            string response;
            context.Response.ContentType = "application/json";

            if (problems.Count == 1)
            {
                var problem = problems.First();
                context.Response.StatusCode = (int)problem.Status!;
                response = JsonSerializer.Serialize(new { problem.Title, problem.Detail, problem.Type });
            }
            else
            {
                context.Response.StatusCode = 400;
                response = JsonSerializer.Serialize(problems);
            }
            await context.Response.WriteAsync(response);
        }
    }
}
