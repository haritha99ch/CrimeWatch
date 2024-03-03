namespace Application.Errors.Reports;
public record ReportNotBookmarkedError : Error<ReportNotBookmarkedError>
{
    public override string Title { get; init; } = "Report is not bookmarked";
    public override string Message { get; init; } = "Cannot remove bookmark when report is not bookmarked.";
    public override HttpStatusCode Code { get; init; } = HttpStatusCode.Conflict;
}
