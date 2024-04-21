using Microsoft.EntityFrameworkCore;
using Persistence.Common.Specifications;

namespace Application.Specifications.Reports;
internal class ReportWithEvidenceById : Specification<Report>
{
    public ReportWithEvidenceById(ReportId reportId, EvidenceId evidenceId, bool includeImages = false) : base(e
        => e.Id.Equals(reportId))
    {
        if (includeImages)
            AddInclude(q => q
                .Include(r => r.Evidences.Where(e => e.Id.Equals(evidenceId)))
                .ThenInclude(e => e.MediaItems));
        else
            AddInclude(q => q
                .Include(r => r.Evidences.Where(e => e.Id.Equals(evidenceId))));
    }
}
