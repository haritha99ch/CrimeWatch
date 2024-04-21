using Persistence.Common.Specifications;

namespace Application.Specifications.Reports;
internal sealed class ReportDetailsById : Specification<Report, ReportDetails>
{
    public ReportDetailsById(ReportId reportId) : base(e => e.Id.Equals(reportId))
    {
        ProjectTo(e => new()
        {
            ReportId = e.Id,
            AuthorDetails = e.AuthorId != null && e.Author != null
                ? new(
                    e.AuthorId,
                    $"{e.Author!.Person!.FirstName} {e.Author.Person.LastName}",
                    e.Author.Email,
                    e.Author.PhoneNumber
                )
                : null,
            ModeratorDetails = e.ModeratorId != null && e.Moderator != null
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
            Status = e.Status,
            BookmarksCount = e.BookmarksCount,
            IsBookmarkedByCurrentUser = false,
            MediaItem = e.MediaItem != null ? new(e.MediaItem!.Url, e.MediaItem.MediaType) : null
        });
    }
}
