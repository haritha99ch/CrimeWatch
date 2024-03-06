namespace Application.Features.Reports.Commands.RevertEvidenceToUnderReview;
public sealed record RevertEvidenceToUnderReviewCommand(ReportId ReportId, EvidenceId EvidenceId) : ICommand<bool>;
