using Persistence.Common.Specifications;

namespace Application.Specifications.Reports;
internal record ReportBookmarkInfoById : Specification<Report, ReportBookmarkInfo>
{
    public ReportBookmarkInfoById(ReportId reportId, AccountId accountId) : base(e => e.Id.Equals(reportId))
    {
        ProjectTo(
            r => new(r.Status, r.Bookmarks.AsQueryable().Any(b => b.AccountId.Equals(accountId)))
        );
    }
}
