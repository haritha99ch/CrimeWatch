using Domain.AggregateModels.ReportAggregate.Enums;
using Domain.AggregateModels.ReportAggregate.ValueObjects;

namespace Domain.AggregateModels.ReportAggregate.Entities;
public sealed class MediaItem : Entity<MediaItemId>
{
    public required string Url { get; init; }
    public required MediaType MediaType { get; init; }

    public static MediaItem Create(string url, MediaType mediaType) => new()
    {
        Id = new(Guid.NewGuid()),
        Url = url,
        MediaType = mediaType,
        CreatedAt = DateTime.Now
    };
}
