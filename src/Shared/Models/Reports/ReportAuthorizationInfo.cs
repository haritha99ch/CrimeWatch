using Domain.AggregateModels.ReportAggregate.Enums;

namespace Shared.Models.Reports;
public class ReportAuthorizationInfo : ISelector
{
    public AccountId? AuthorId { get; set; }
    public AccountId? ModeratorId { get; set; }
    public Status Status { get; set; }
}
