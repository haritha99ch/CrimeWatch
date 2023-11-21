using Domain.AggregateModels.ReportAggregate.Enums;
using Domain.AggregateModels.ReportAggregate.ValueObjects;

namespace Domain.AggregateModels.ReportAggregate.Entities;

public sealed record MediaItem : Entity<MediaItemId>
{
    public required string Url { get; set; }
    public required MediaType MediaType { get; set; }

    public static MediaItem Create(string url, MediaType mediaType) =>
        new()
        {
            Id = new(Guid.NewGuid()),
            Url = url,
            MediaType = mediaType,
            CreatedAt = DateTime.Now
        };

    public bool Update(string url, MediaType mediaType)
    {
        if (url.Equals(Url) && mediaType.Equals(MediaType))
            return false;

        Url = url;
        MediaType = mediaType;
        UpdatedAt = DateTime.Now;

        return true;
    }
}
