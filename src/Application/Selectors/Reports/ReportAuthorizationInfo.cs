using System.Linq.Expressions;

namespace Application.Selectors.Reports;
public sealed record ReportAuthorizationInfo(AccountId? AuthorId, AccountId? ModeratorId, Status Status)
    : ISelector<Report, ReportAuthorizationInfo>
{
    public Expression<Func<Report, ReportAuthorizationInfo>> SetProjection()
        => e => new(e.AuthorId, e.ModeratorId, e.Status);
}
