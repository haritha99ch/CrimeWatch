using Persistence.Common.Specifications;

namespace Application.Specifications.Reports;
internal record ReportDetailsById : Specification<Report, ReportDetails>
{
    public ReportDetailsById(ReportId reportId) : base(e => e.Id.Equals(reportId))
    {
        Select = e => new(
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
            e.Status,
            e.BookmarksCount,
            false,
            new(e.MediaItem!.Url, e.MediaItem.MediaType)
        );
    }
}
