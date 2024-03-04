namespace Application.Errors.Reports;
public record EvidenceAlreadyModeratedError : Error<EvidenceAlreadyModeratedError>
{
    public override string Title { get; init; } = "Evidence is already moderated.";
    public override string Message { get; init; } = "Cannot moderate already moderated evidence.";
    public override HttpStatusCode Code { get; init; } = HttpStatusCode.Conflict;
}
