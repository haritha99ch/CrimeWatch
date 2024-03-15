﻿using System.Linq.Expressions;

namespace Application.Selectors.Reports;
public sealed class ReportDetails : ReportDto.ReportDetails, ISelector<Report, ReportDetails>
{
    public Expression<Func<Report, ReportDetails>> SetProjection()
        => e => new()
        {
            ReportId = e.Id,
            AuthorDetails = e.AuthorId == null
                ? null
                : new(
                    e.AuthorId,
                    $"{e.Author!.Person!.FirstName} {e.Author.Person.LastName}",
                    e.Author.Email,
                    e.Author.PhoneNumber
                ),
            ModeratorDetails = e.ModeratorId == null
                ? null
                : new(
                    e.ModeratorId,
                    $"{e.Moderator!.Person!.FirstName} {e.Moderator.Person.FirstName}",
                    e.Moderator.Email,
                    e.Moderator.PhoneNumber,
                    e.Moderator.Moderator!.City,
                    e.Moderator.Moderator.Province
                ),
            Caption = e.Caption,
            Description = e.Description,
            Location = e.Location,
            Status = e.Status,
            BookmarksCount = e.BookmarksCount,
            IsBookmarkedByCurrentUser = false,
            MediaItem = e.MediaItem != null ? new(e.MediaItem!.Url, e.MediaItem.MediaType) : null
        };
}
