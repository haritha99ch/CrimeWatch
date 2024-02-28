using Domain.AggregateModels.ReportAggregate.Entities;
using Persistence.Common.Selectors;
using System.Linq.Expressions;

namespace Application.Selectors.Reports;
public record EvidenceAuthorizationInfo(
        EvidenceId EvidenceId,
        AccountId? AuthorId,
        AccountId? ModeratorId,
        Status Status
    ) : Selector<Evidence, EvidenceAuthorizationInfo>, ISelector
{
    protected override Expression<Func<Evidence, EvidenceAuthorizationInfo>> SetProjection()
        => e => new(e.Id, e.AuthorId, e.ModeratorId, e.Status);

}
