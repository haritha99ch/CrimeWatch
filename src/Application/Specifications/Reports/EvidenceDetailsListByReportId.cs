using Persistence.Common.Specifications;
using Persistence.Common.Specifications.Types;

namespace Application.Specifications.Reports;
internal record EvidenceDetailsListByReportId : Specification<Report, EvidenceDetails>
{
    public EvidenceDetailsListByReportId(
            bool moderated,
            ReportId reportId,
            AccountId? currentUser = null,
            Pagination? pagination = null
        )
        : base(e => e.Id.Equals(reportId))
    {
        pagination ??= new(0, int.MaxValue);
        Select = new(r => r.Evidences
            .AsQueryable()
            .Where(e => moderated
                || e.Status.Equals(Status.Approved)
                || e.Author != null && e.Author.Id.Equals(currentUser))
            .Skip(pagination.Skip)
            .Take(pagination.Take)
            .Select(EvidenceDetails.GetProjection)
            .ToList());
    }

}
