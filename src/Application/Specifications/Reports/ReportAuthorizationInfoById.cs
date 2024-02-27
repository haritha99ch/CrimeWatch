using Persistence.Common.Specifications;

namespace Application.Specifications.Reports;
internal record ReportAuthorizationInfoById : Specification<Report, ReportAuthorizationInfo>
{
    public ReportAuthorizationInfoById(ReportId reportId) : base(e => e.Id.Equals(reportId))
    {
        Select = ReportAuthorizationInfo.GetProjection;
    }
}
