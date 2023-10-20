namespace CrimeWatch.Application.Commands.EvidenceCommands.AddCommentToEvidence;
public record AddCommentToEvidenceCommand(EvidenceId EvidenceId, string Comment) : IRequest<Evidence>;
