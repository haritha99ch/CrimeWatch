using Microsoft.EntityFrameworkCore;
using Persistence.Common.Specifications;

namespace Application.Specifications.Reports;
internal record ReportWithEvidenceById : Specification<Report>
{
    public ReportWithEvidenceById(ReportId reportId, EvidenceId evidenceId) : base(e => e.Id.Equals(reportId))
    {
        AddInclude(q => q.Include(r => r.Evidences.AsQueryable().Where(e => e.Id.Equals(evidenceId))));
    }
}
