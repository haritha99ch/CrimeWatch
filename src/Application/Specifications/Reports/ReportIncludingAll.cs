using Microsoft.EntityFrameworkCore;
using Persistence.Common.Specifications;

namespace Application.Specifications.Reports;
internal sealed class ReportIncludingAll : Specification<Report>
{
    public ReportIncludingAll(ReportId reportId) : base(e => e.Id.Equals(reportId))
    {
        AddInclude(q => q.Include(e => e.MediaItem!));
    }
}
