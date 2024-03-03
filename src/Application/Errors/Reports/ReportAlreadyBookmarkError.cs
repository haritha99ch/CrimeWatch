namespace Application.Errors.Reports;
public record ReportAlreadyBookmarkError : Error<ReportAlreadyBookmarkError>
{
    public override string Title { get; init; } = "Bookmark error";
    public override string Message { get; init; } = "Cannot bookmark report";
    public override HttpStatusCode Code { get; init; } = HttpStatusCode.Conflict;
}
