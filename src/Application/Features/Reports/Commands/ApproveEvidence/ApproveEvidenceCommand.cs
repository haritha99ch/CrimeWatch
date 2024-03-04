namespace Application.Features.Reports.Commands.ApproveEvidence;
public sealed record ApproveEvidenceCommand(ReportId ReportId, EvidenceId EvidenceId) : ICommand<bool>;
