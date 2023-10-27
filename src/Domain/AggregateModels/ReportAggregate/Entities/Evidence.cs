using Domain.AggregateModels.AccountAggregate;
using Domain.AggregateModels.AccountAggregate.ValueObjects;
using Domain.AggregateModels.ReportAggregate.Enums;
using Domain.AggregateModels.ReportAggregate.ValueObjects;

namespace Domain.AggregateModels.ReportAggregate.Entities;
public sealed record Evidence : Entity<EvidenceId>
{
    public required AccountId AuthorId { get; init; }
    public AccountId? ModeratorId { get; init; }
    public required string Caption { get; init; }
    public required string Description { get; init; }
    public required Location Location { get; init; }
    public required Status Status { get; init; }

    public List<MediaItem> MediaItems { get; init; } = new();
    public Account? Author { get; init; }
    public Account? Moderator { get; init; }

    public static Evidence Create(
        AccountId authorId,
        string caption,
        string description,
        Location location,
        List<MediaItem>? mediaItems) => new()
    {
        AuthorId = authorId,
        Caption = caption,
        Description = description,
        Location = location,
        MediaItems = mediaItems ?? new(),
        Status = Status.Pending,
        CreatedAt = DateTime.Now,
        Id = new(Guid.NewGuid())
    };

    public Evidence Update(
        string caption,
        string description,
        Location location,
        List<MediaItem>? mediaItems)
    {
        mediaItems ??= new();
        if (caption.Equals(Caption)
            && description.Equals(Description)
            && location.Equals(Location)
            && mediaItems.OrderBy(e => e).SequenceEqual(MediaItems.OrderBy(e => e)))
            return this;

        return this with
        {
            Caption = caption,
            Description = description,
            Location = location,
            MediaItems = mediaItems,
            UpdatedAt = DateTime.Now
        };
    }

}
