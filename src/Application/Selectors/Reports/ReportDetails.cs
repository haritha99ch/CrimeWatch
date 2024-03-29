﻿using Persistence.Common.Selectors;
using Shared.Dto.MediaItems;
using System.Linq.Expressions;

namespace Application.Selectors.Reports;
public record ReportDetails(
        ReportId ReportId,
        WitnessDetailsForReportDetails? AuthorDetails,
        ModeratorDetailsForReportDetails? ModeratorDetails,
        string Caption,
        string Description,
        Location Location,
        Status Status,
        int BookmarksCount,
        bool IsBookmarkedByCurrentUser, // Handle in request handler
        MediaViewItem MediaItem
    ) : Selector<Report, ReportDetails>, ISelector
{
    protected override Expression<Func<Report, ReportDetails>> SetProjection()
        => e => new(
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
