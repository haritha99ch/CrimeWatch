namespace CrimeWatch.Application.Commands.EvidenceCommands.RevertEvidenceToReview;
public sealed record RevertEvidenceToReviewCommand(EvidenceId EvidenceId) : IRequest<Evidence>;
