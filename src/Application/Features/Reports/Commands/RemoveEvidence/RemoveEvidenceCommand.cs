namespace Application.Features.Reports.Commands.RemoveEvidence;
public sealed record RemoveEvidenceCommand(ReportId ReportId, EvidenceId EvidenceId) : ICommand<bool>;
