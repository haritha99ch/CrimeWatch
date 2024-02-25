using Persistence.Common.Specifications;
using Persistence.Common.Specifications.Types;
using Shared.Dto.MediaItems;

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
        SelectList = r => r.Evidences
            .Where(e => moderated
                || e.Status.Equals(Status.Approved)
                || e.Author != null && e.Author.Id.Equals(currentUser))
            .Skip(pagination.Skip)
            .Take(pagination.Take)
            .Select(e => new EvidenceDetails(
                e.Id,
                e.AuthorId == null
                    ? null
                    : new(
                        e.AuthorId,
                        $"{e.Author!.Person!.FirstName} {e.Author.Person.LastName}",
                        e.Author.Email,
                        e.Author.PhoneNumber
                    ),
                e.ModeratorId == null
                    ? null
                    : new(
                        e.ModeratorId,
                        $"{e.Moderator!.Person!.FirstName} {e.Moderator.Person.FirstName}",
                        e.Moderator.Email,
                        e.Moderator.PhoneNumber,
                        e.Moderator.Moderator!.City,
                        e.Moderator.Moderator.Province
                    ),
                e.Caption,
                e.Description,
                e.Location,
                e.MediaItems
                    .Select(m => new MediaViewItem(m.Url, m.MediaType))
                    .ToList()))
            .ToList();
    }

}
