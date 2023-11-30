using System.Net;
using Application.Common.Errors;

namespace Application.Errors.Common;

public sealed record ValidationError : Error<ValidationError>
{
    public override string Title { get; init; } = "Validation failed";
    public override string Message { get; init; } =
        "An error has occurred when validating a request.";
    public override HttpStatusCode Code { get; init; } = HttpStatusCode.BadRequest;
}
