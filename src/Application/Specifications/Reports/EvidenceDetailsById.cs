using Microsoft.EntityFrameworkCore;
using Persistence.Common.Specifications;

namespace Application.Specifications.Reports;
internal record EvidenceDetailsById : Specification<Report, EvidenceDetails>
{
    public EvidenceDetailsById(ReportId reportId, EvidenceId evidenceId) : base(r => r.Id.Equals(reportId))
    {
        AddInclude(q => q.Include(r => r.Evidences));
        Select = new(r => r.Evidences
            .AsQueryable()
            .Select(EvidenceDetails.GetProjection)
            .FirstOrDefault()!);
    }
}
