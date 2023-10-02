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

            if (problems.Count == 1)
            {
                var problem = problems.First();
                context.Response.StatusCode = (int)problem.Status!;
            }
            else
            {
                var problem = problems.First();
                var status = problem.Status;
                if (problems.Any(problemDetail => problemDetail.Status != status)) status = 400;
                context.Response.StatusCode = (int)status!;
            }

            context.Response.ContentType = "application/json";
            var response = JsonSerializer.Serialize(problems);
            await context.Response.WriteAsync(response);
        }
    }
}
