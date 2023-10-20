namespace CrimeWatch.Application.Commands.EvidenceCommands.ApproveEvidence;
public sealed record ApproveEvidenceCommand(EvidenceId EvidenceId) : IRequest<Evidence>;
