namespace Application.Errors.Accounts;

public sealed record UnableToAuthenticateError : Error<UnableToAuthenticateError>
{
    public override string Title { get; init; } = "Authentication Failed.";
    public override string Message { get; init; } = "Cannot read the token";
    public override HttpStatusCode Code { get; init; } = HttpStatusCode.Unauthorized;
}
