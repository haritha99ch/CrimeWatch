using Domain.AggregateModels.ReportAggregate.Enums;
using Domain.AggregateModels.ReportAggregate.ValueObjects;

namespace Shared.Dto.Reports;
public class EvidenceAuthorizationInfo
{
    public required EvidenceId EvidenceId { get; init; }
    public AccountId? AuthorId { get; init; }
    public AccountId? ModeratorId { get; init; }
    public Status Status { get; init; }
}
