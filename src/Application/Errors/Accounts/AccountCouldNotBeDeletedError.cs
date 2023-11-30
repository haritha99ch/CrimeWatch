namespace Application.Errors.Accounts;

public sealed record AccountCouldNotBeDeletedError : Error<AccountCouldNotBeDeletedError>
{
    public override string Title { get; init; } = "Account could not be deleted";
    public override string Message { get; init; } =
        "An error has occurred when deleting the account";
    public override HttpStatusCode Code { get; init; } = HttpStatusCode.InternalServerError;
}
