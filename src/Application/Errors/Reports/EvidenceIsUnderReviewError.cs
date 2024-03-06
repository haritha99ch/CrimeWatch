namespace Application.Errors.Reports;
public record EvidenceIsUnderReviewError : Error<EvidenceIsUnderReviewError>
{
    public override string Title { get; init; } = "Evidence is under review.";
    public override string Message { get; init; } = "Evidence is already under review";
    public override HttpStatusCode Code { get; init; } = HttpStatusCode.Conflict;
}
