using Domain.AggregateModels.ReportAggregate.Enums;
using Domain.AggregateModels.ReportAggregate.ValueObjects;

namespace Domain.AggregateModels.ReportAggregate.Entities;
public sealed record MediaItem : Entity<MediaItemId>
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

    public MediaItem Update(string url, MediaType mediaType)
    {
        if (url.Equals(Url) && mediaType.Equals(MediaType)) return this;
        return this with
        {
            Url = url,
            MediaType = mediaType,
            UpdatedAt = DateTime.Now
        };
    }
}
