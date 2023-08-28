namespace CrimeWatch.Application.Commands.DeleteEvidence;
public sealed record DeleteEvidenceCommand(EvidenceId Id) : IRequest<bool>;
