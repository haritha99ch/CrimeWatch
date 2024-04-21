using Persistence.Common.Specifications;

namespace Application.Specifications.Reports;
internal sealed class EvidenceAuthorizationInfoById : Specification<Report, EvidenceAuthorizationInfo>
{
    public EvidenceAuthorizationInfoById(ReportId reportId, EvidenceId evidenceId)
        : base(r => r.Id.Equals(reportId))
    {
        ProjectTo(r => r.Evidences
            .AsQueryable()
            .Where(e => e.Id.Equals(evidenceId))
            .Select(e => new EvidenceAuthorizationInfo
            {
                EvidenceId = e.Id,
                AuthorId = e.AuthorId,
                ModeratorId = e.ModeratorId,
                Status = e.Status
            })
            .First());
    }
}
