using Persistence.Common.Specifications;
using Shared.Models.MediaItems;

namespace Application.Specifications.Reports;
internal sealed class EvidenceDetailsById : Specification<Report, EvidenceDetails>
{
    public EvidenceDetailsById(ReportId reportId, EvidenceId evidenceId) : base(r => r.Id.Equals(reportId))
    {
        ProjectTo(r => r.Evidences
            .AsQueryable()
            .Where(e => e.Id.Equals(evidenceId))
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
            .First());
    }
}
