namespace CrimeWatch.Application.Commands.EvidenceCommands.DeleteEvidence;
public sealed record DeleteEvidenceCommand(EvidenceId Id) : IRequest<bool>;
