namespace CrimeWatch.Domain.Models.ReportModel;
public class MediaItem : Entity<MediaItemId>
{
    public ReportId? ReportId { get; set; }
    public EvidenceId? EvidenceId { get; set; }
    public MediaItemType Type { get; set; }
    public string Url { get; set; } = string.Empty;

    public static MediaItem CreateForReport(MediaItemType type, string url, ReportId reportId)
    {
        return new MediaItem
        {
            Id = new(new()),
            ReportId = reportId,
            Type = type,
            Url = url
        };
    }

    public static MediaItem CreateForEvidence(MediaItemType type, string url, EvidenceId evidenceId)
    {
        return new MediaItem
        {
            Id = new(new()),
            EvidenceId = evidenceId,
            Type = type,
            Url = url
        };
    }
}
