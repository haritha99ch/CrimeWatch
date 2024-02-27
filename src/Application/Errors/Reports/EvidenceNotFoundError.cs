namespace Application.Errors.Reports;
public record EvidenceNotFoundError : Error<EvidenceNotFoundError>
{
    public override string Title { get; init; } = "Evidence not found";
    public override string Message { get; init; } = "Requested evidence is not found";
    public override HttpStatusCode Code { get; init; } = HttpStatusCode.NotFound;
}
