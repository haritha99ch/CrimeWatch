using Persistence.Common.Specifications;

namespace Application.Specifications.Reports;
internal record EvidenceAuthorizationInfoById : Specification<Report, EvidenceAuthorizationInfo>
{
    public EvidenceAuthorizationInfoById(ReportId reportId, EvidenceId evidenceId)
        : base(r => r.Id.Equals(reportId))
    {
        Select = new(r => r.Evidences
            .AsQueryable()
            .Where(e => e.Id.Equals(evidenceId))
            .Select(EvidenceAuthorizationInfo.GetProjection)
            .FirstOrDefault()!
        );
    }
}
