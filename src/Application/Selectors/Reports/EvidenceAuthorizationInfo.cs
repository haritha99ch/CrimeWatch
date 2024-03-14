using Domain.AggregateModels.ReportAggregate.Entities;
using System.Linq.Expressions;

namespace Application.Selectors.Reports;
public record EvidenceAuthorizationInfo(
        EvidenceId EvidenceId,
        AccountId? AuthorId,
        AccountId? ModeratorId,
        Status Status
    ) : ISelector<Evidence, EvidenceAuthorizationInfo>
{
    public Expression<Func<Evidence, EvidenceAuthorizationInfo>> SetProjection()
        => e => new(e.Id, e.AuthorId, e.ModeratorId, e.Status);

}
