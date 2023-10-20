namespace CrimeWatch.Application.Commands.EvidenceCommands.DeclineEvidence;
public sealed record DeclineEvidenceCommand(EvidenceId EvidenceId) : IRequest<Evidence>;
