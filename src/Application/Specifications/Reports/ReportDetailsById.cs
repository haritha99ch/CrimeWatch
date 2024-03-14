using Persistence.Common.Specifications;

namespace Application.Specifications.Reports;
internal record ReportDetailsById : Specification<Report, ReportDetails>
{
    public ReportDetailsById(ReportId reportId) : base(e => e.Id.Equals(reportId))
    {
        ProjectTo(GetProjection<Report, ReportDetails>());
    }
}
