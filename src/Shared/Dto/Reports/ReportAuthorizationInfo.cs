using Domain.AggregateModels.ReportAggregate.Enums;

namespace Shared.Dto.Reports;
public class ReportAuthorizationInfo
{
    public AccountId? AuthorId { get; set; }
    public AccountId? ModeratorId { get; set; }
    public Status Status { get; set; }
}
