namespace Application.Errors.Reports;
public sealed record ReportCouldNotBeDeletedError : Error<ReportCouldNotBeDeletedError>
{
    public override string Title { get; init; } = "Could not delete the account";
    public override string Message { get; init; } = "An error has occurred when deleting the report";
    public override HttpStatusCode Code { get; init; } = HttpStatusCode.InternalServerError;
}
