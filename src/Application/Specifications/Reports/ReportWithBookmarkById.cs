using Microsoft.EntityFrameworkCore;
using Persistence.Common.Specifications;

namespace Application.Specifications.Reports;
internal sealed record ReportWithBookmarkById : Specification<Report>
{
    public ReportWithBookmarkById(ReportId reportId, AccountId accountId) : base(e => e.Id.Equals(reportId))
    {
        AddInclude(q => q.Include(r => r.Bookmarks.Where(b => b.AccountId.Equals(accountId))));
    }
}
