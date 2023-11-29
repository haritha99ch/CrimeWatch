using System.Net;
using Application.Common.Errors;

namespace Application.Features.Accounts.Queries.GetAccountById.Errors;

public record UnauthorizedError : Error<UnauthorizedError>
{
    public override string Title { get; init; } = "Unauthorized Request";
    public override string Message { get; init; } = "You are not authorized to perform this action.";
    public override HttpStatusCode Code { get; init; } = HttpStatusCode.Unauthorized;
}
