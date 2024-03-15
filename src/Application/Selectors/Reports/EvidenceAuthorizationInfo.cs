using Domain.AggregateModels.ReportAggregate.Entities;
using System.Linq.Expressions;

namespace Application.Selectors.Reports;
public sealed class EvidenceAuthorizationInfo : ReportDto.EvidenceAuthorizationInfo,
    ISelector<Evidence, EvidenceAuthorizationInfo>
{
    public Expression<Func<Evidence, EvidenceAuthorizationInfo>> SetProjection()
        => e => new()
        {
            EvidenceId = e.Id,
            AuthorId = e.AuthorId,
            ModeratorId = e.ModeratorId,
            Status = e.Status
        };
}
