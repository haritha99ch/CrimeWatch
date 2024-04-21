using Persistence.Common.Specifications;
using Persistence.Common.Utilities;
using Shared.Models.MediaItems;

namespace Application.Specifications.Reports;
internal sealed class EvidenceDetailsListByReportId : Specification<Report, EvidenceDetails>
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
        ProjectTo(r => r.Evidences
            .AsQueryable()
            .Where(e => moderated
                || e.Status.Equals(Status.Approved)
                || e.Author != null && e.Author.Id.Equals(currentUser))
            .Skip(pagination.Skip)
            .Take(pagination.Take)
            .Select(e => new EvidenceDetails
            {
                EvidenceId = e.Id,
                Author = e.AuthorId != null && e.Author != null
                    ? new(
                        e.AuthorId,
                        $"{e.Author!.Person!.FirstName} {e.Author.Person.LastName}",
                        e.Author.Email,
                        e.Author.PhoneNumber
                    )
                    : null,
                Moderator = e.ModeratorId != null && e.Moderator != null
                    ? new(
                        e.ModeratorId,
                        $"{e.Moderator!.Person!.FirstName} {e.Moderator.Person.FirstName}",
                        e.Moderator.Email,
                        e.Moderator.PhoneNumber,
                        e.Moderator.Moderator!.City,
                        e.Moderator.Moderator.Province
                    )
                    : null,
                Caption = e.Caption,
                Description = e.Description,
                Location = e.Location,
                MediaItems = e.MediaItems
                    .AsQueryable()
                    .Select(m => new MediaViewItem(m.Url, m.MediaType))
                    .ToList()
            })
            .ToList());
    }

}
