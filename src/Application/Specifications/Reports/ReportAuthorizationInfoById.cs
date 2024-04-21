using Persistence.Common.Specifications;

namespace Application.Specifications.Reports;
internal sealed class ReportAuthorizationInfoById : Specification<Report, ReportAuthorizationInfo>
{
    public ReportAuthorizationInfoById(ReportId reportId) : base(e => e.Id.Equals(reportId))
    {
        ProjectTo(e => new()
        {
            AuthorId = e.AuthorId,
            ModeratorId = e.ModeratorId,
            Status = e.Status
        });
    }
}
