namespace Application.Features.Reports.Commands.ModerateEvidence;
public sealed record ModerateEvidenceCommand(ReportId ReportId, EvidenceId EvidenceId, AccountId AccountId)
    : ICommand<bool>;
