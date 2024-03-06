namespace Application.Features.Reports.Commands.DeclineEvidence;
public sealed record DeclineEvidenceCommand(ReportId ReportId, EvidenceId EvidenceId) : ICommand<bool>;
