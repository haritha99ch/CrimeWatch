using Persistence.Common.Specifications;

namespace Application.Specifications.Reports;
public record ReportDetailsList : Specification<Report, ReportDetails>
{
    public ReportDetailsList(bool moderated, AccountId? currentUser = null)
        : base(e => moderated
            || e.Status.Equals(Status.Approved)
            || e.AuthorId != null && e.AuthorId.Equals(currentUser))
    {
        ProjectTo(GetProjection<Report, ReportDetails>());
    }
}
