namespace Application.Errors.Reports;
public record ReportIsNoUnderReviewError : Error<ReportIsNoUnderReviewError>
{
    public override string Title { get; init; } = "Report is not under reviewed.";
    public override string Message { get; init; } = "Report must be under reviewed.";
    public override HttpStatusCode Code { get; init; } = HttpStatusCode.BadRequest;
}
