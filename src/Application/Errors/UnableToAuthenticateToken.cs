using Application.Common.Errors;

namespace Application.Errors;

public sealed record UnableToAuthenticateTokenError : Error<UnableToAuthenticateTokenError>
{
    public override string Title { get; init; } = "Authentication Failed.";
    public override string Message { get; init; } = "Cannot read the accountId";
    public override int Code { get; init; } = 401;
}
