using Persistence.Common.Specifications;

namespace Application.Specifications.Reports;
internal record EvidenceDetailsById : Specification<Report, EvidenceDetails>
{
    public EvidenceDetailsById(ReportId reportId, EvidenceId evidenceId) : base(r => r.Id.Equals(reportId))
    {
        ProjectTo(r => r.Evidences
            .AsQueryable()
            .Where(e => e.Id.Equals(evidenceId))
            .Select(EvidenceDetails.Projection)
            .First());
    }
}
