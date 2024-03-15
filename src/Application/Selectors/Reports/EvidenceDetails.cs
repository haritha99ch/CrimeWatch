using Domain.AggregateModels.ReportAggregate.Entities;
using Shared.Dto.MediaItems;
using System.Linq.Expressions;

namespace Application.Selectors.Reports;
public sealed class EvidenceDetails : ReportDto.EvidenceDetails, ISelector<Evidence, EvidenceDetails>
{
    public Expression<Func<Evidence, EvidenceDetails>> SetProjection()
        => e => new()
        {
            EvidenceId = e.Id,
            Author = e.AuthorId == null
                ? null
                : new(
                    e.AuthorId,
                    $"{e.Author!.Person!.FirstName} {e.Author.Person.LastName}",
                    e.Author.Email,
                    e.Author.PhoneNumber
                ),
            Moderator = e.ModeratorId == null
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
            MediaItems = e.MediaItems
                .AsQueryable()
                .Select(m => new MediaViewItem(m.Url, m.MediaType))
                .ToList()
        };

}
