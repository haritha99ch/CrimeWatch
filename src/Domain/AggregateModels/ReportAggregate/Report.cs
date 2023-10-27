using Domain.AggregateModels.AccountAggregate;
using Domain.AggregateModels.AccountAggregate.ValueObjects;
using Domain.AggregateModels.ReportAggregate.Entities;
using Domain.AggregateModels.ReportAggregate.Enums;
using Domain.AggregateModels.ReportAggregate.ValueObjects;

namespace Domain.AggregateModels.ReportAggregate;
public sealed record Report : AggregateRoot<ReportId>
{
    public required AccountId AuthorId { get; init; }
    public AccountId? ModeratorId { get; init; }
    public required string Caption { get; init; }
    public required string Description { get; init; }
    public required Location Location { get; init; }
    public required Status Status { get; init; }

    public List<Evidence> Evidences { get; init; } = new();
    public MediaItem? MediaItem { get; init; }
    public Account? Author { get; init; }
    public Account? Moderator { get; init; }

    public static Report Create(
        AccountId authorId,
        string caption,
        string description,
        Location location,
        MediaItem mediaItem) => new()
    {
        Id = new(Guid.NewGuid()),
        AuthorId = authorId,
        Caption = caption,
        Description = description,
        Location = location,
        MediaItem = mediaItem,
        Status = Status.Pending,
        CreatedAt = DateTime.Now
    };

    public Report Update(string caption, string description, Location location, MediaItem mediaItem)
    {
        var updatedMediaItem = MediaItem!.Update(mediaItem.Url, mediaItem.MediaType);
        if (caption.Equals(Caption)
            && description.Equals(Description)
            && location.Equals(Location)
            && updatedMediaItem.Equals(MediaItem)) return this;

        return this with
        {
            Caption = caption,
            Description = description,
            Location = location
        };
    }
}
