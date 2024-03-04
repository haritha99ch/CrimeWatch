namespace Application.Errors.Reports;
public record EvidenceIsNoUnderReviewError : Error<EvidenceIsNoUnderReviewError>
{
    public override string Title { get; init; } = "Evidence is not under reviewed.";
    public override string Message { get; init; } = "Evidence must be under reviewed.";
    public override HttpStatusCode Code { get; init; } = HttpStatusCode.BadRequest;
}
