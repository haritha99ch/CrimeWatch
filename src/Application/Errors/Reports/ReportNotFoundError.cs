namespace Application.Errors.Reports;
public sealed record ReportNotFoundError : Error<ReportNotFoundError>
{
    public override string Title { get; init; } = "Report not found";
    public override string Message { get; init; } = "Requested report is not found";
    public override HttpStatusCode Code { get; init; } = HttpStatusCode.NotFound;
}
