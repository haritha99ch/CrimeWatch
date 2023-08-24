namespace CrimeWatch.Domain.AggregateModels.ReportAggregate;
public class Evidence : Entity<EvidenceId>
{
    public WitnessId WitnessId { get; set; } = default!;
    public ModeratorId ModeratorId { get; set; } = default!;
    public ReportId ReportId { get; set; } = default!;
    public string Caption { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime DateTime { get; set; } = DateTime.Now;
    public Location Location { get; set; } = default!;
    public Status Status { get; set; } = Status.Pending;
    public string ModeratorComment { get; set; } = string.Empty;

    public Moderator? Moderator { get; set; }
    public Witness? Witness { get; set; }
    public Report? Report { get; set; }
    public List<MediaItem> MediaItems { get; set; } = new();

    public static Evidence Create(
        WitnessId authorId,
        ReportId reportId,
        string caption,
        string description,
        Location location,
        List<MediaItem> mediaItems
        ) => new()
        {
            Id = new(new()),
            WitnessId = authorId,
            ReportId = reportId,
            Caption = caption,
            Description = description,
            Location = location,
            MediaItems = mediaItems
        };

    public void AddMediaItem(MediaItem mediaItem) => MediaItems.Add(mediaItem);
    public void Moderate(ModeratorId moderatorId)
    {
        ModeratorId = moderatorId;
        Status = Status.UnderReview;
    }
    public void Approve() => Status = Status.Approved;
    public void Decline() => Status = Status.Declined;
    public void Comment(string comment) => ModeratorComment = comment;
}
