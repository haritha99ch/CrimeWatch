using System.Linq.Expressions;

namespace Application.Selectors.Reports;
public sealed class ReportAuthorizationInfo
    : ReportDto.ReportAuthorizationInfo, ISelector<Report, ReportAuthorizationInfo>
{
    public Expression<Func<Report, ReportAuthorizationInfo>> SetProjection()
        => e => new()
        {
            AuthorId = e.AuthorId,
            ModeratorId = e.ModeratorId,
            Status = e.Status
        };
}
