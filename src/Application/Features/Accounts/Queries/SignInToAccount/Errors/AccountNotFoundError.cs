using Application.Common.Errors;

namespace Application.Features.Accounts.Queries.SignInToAccount.Errors;

public sealed record AccountNotFoundError : Error<AccountNotFoundError>
{
    public override string Title { get; init; } = "Account not found";
    public override string Message { get; init; } = "Invalid email or password";
    public override int Code { get; init; } = 404;
}
