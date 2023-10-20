namespace CrimeWatch.Domain.AggregateModels.ReportAggregate;
public class MediaItem : Entity<MediaItemId>
{
    public MediaItemType Type { get; set; }
    public string Url { get; set; } = string.Empty;

    public static MediaItem? Create(MediaItemType type, string url) => new()
    {
        Id = new(Guid.NewGuid()),
        Type = type,
        Url = url
    };
}
