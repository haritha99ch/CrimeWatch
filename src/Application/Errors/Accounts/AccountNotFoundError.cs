using System.Net;
using Application.Common.Errors;

namespace Application.Errors.Accounts;

public sealed record AccountNotFoundError : Error<AccountNotFoundError>
{
    public override string Title { get; init; } = "Account not found";
    public override string Message { get; init; } = "Invalid email or password";
    public override HttpStatusCode Code { get; init; } = HttpStatusCode.NotFound;
}
