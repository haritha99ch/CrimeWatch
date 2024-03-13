using Application.Common.Errors;

namespace Web.API.Helpers.Controllers;
internal static class ControllerHelper
{
    public static ObjectResult ToProblemDetails(this Error error)
    {
        var problemDetails = new ProblemDetails
        {
            Detail = error.Message,
            Status = (int)error.Code,
            Title = error.Title
        };
        return new(problemDetails);
    }
}
