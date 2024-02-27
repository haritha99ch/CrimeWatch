using Persistence.Common.Selectors;
using System.Linq.Expressions;

namespace Application.Selectors.Reports;
public sealed record ReportAuthorizationInfo(AccountId? AuthorId, AccountId? ModeratorId, Status Status)
    : Selector<Report, ReportAuthorizationInfo>, ISelector
{
    protected override Expression<Func<Report, ReportAuthorizationInfo>> SetProjection()
        => e => new(e.AuthorId, e.ModeratorId, e.Status);
}
