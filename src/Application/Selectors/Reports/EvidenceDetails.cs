using Domain.AggregateModels.ReportAggregate.Entities;
using Shared.Dto.MediaItems;
using System.Linq.Expressions;

namespace Application.Selectors.Reports;
public record EvidenceDetails(
        EvidenceId EvidenceId,
        WitnessDetailsForReportDetails? Author,
        ModeratorDetailsForReportDetails? Moderator,
        string Caption,
        string Description,
        Location Location,
        List<MediaViewItem> MediaItems
    ) : ISelector<Evidence, EvidenceDetails>
{
    public Expression<Func<Evidence, EvidenceDetails>> SetProjection()
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
            e.MediaItems
                .AsQueryable()
                .Select(m => new MediaViewItem(m.Url, m.MediaType))
                .ToList());

}
