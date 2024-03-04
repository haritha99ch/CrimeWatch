namespace Application.Errors.Reports;
public record ReportIsUnderReviewError : Error<ReportIsUnderReviewError>
{
    public override string Title { get; init; } = "Report is under review.";
    public override string Message { get; init; } = "Report is already under review";
    public override HttpStatusCode Code { get; init; } = HttpStatusCode.Conflict;
}
