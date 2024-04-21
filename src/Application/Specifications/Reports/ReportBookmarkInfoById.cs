using Persistence.Common.Specifications;

namespace Application.Specifications.Reports;
internal sealed class ReportBookmarkInfoById : Specification<Report, ReportBookmarkInfo>
{
    public ReportBookmarkInfoById(ReportId reportId, AccountId accountId) : base(e => e.Id.Equals(reportId))
    {
        ProjectTo(r => new()
            {
                ReportStatus = r.Status,
                AlreadyBookmarked = r.Bookmarks.AsQueryable().Any(b => b.AccountId.Equals(accountId))
            }
        );
    }
}
