using Domain.AggregateModels.ReportAggregate.Enums;
using Domain.AggregateModels.ReportAggregate.ValueObjects;

namespace Shared.Models.Reports;
public class EvidenceAuthorizationInfo : ISelector
{
    public required EvidenceId EvidenceId { get; init; }
    public AccountId? AuthorId { get; init; }
    public AccountId? ModeratorId { get; init; }
    public Status Status { get; init; }
}
