namespace CrimeWatch.Application.Commands.EvidenceCommands.CreateEvidence;
public sealed record CreateEvidenceCommand(
        WitnessId WitnessId,
        ReportId ReportId,
        string Caption,
        string Description,
        Location Location,
        List<IFormFile> MediaItems
    ) : IRequest<Evidence>;
