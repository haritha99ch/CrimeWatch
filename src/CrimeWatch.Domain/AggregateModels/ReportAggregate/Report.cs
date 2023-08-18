namespace CrimeWatch.Domain.AggregateModels.ReportAggregate;
public class Report : AggregateRoot<ReportId>
{
    public WitnessId AuthorId { get; set; } = default!;
    public ModeratorId ModeratorId { get; set; } = default!;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public Location Location { get; set; } = new();
    public Status Status { get; set; } = Status.Pending;
    public string ModeratorComment { get; set; } = string.Empty;

    public Witness? Author { get; set; }
    public Moderator? Moderator { get; set; }
    public MediaItem? MediaItem { get; set; }
    public List<Evidence> Evidences { get; set; } = new();

    public static Report Create(
        WitnessId authorId,
        string title,
        string description,
        DateTime date,
        Location location,
        MediaItem mediaItem
        ) => new()
        {
            Id = new(new()),
            AuthorId = authorId,
            Title = title,
            Description = description,
            Date = date,
            Location = location,
            MediaItem = mediaItem
        };

    public void AddEvidence(Evidence evidence) => Evidences.Add(evidence);
    public void Moderate(ModeratorId moderatorId)
    {
        ModeratorId = moderatorId;
        Status = Status.UnderReview;
    }

    public void Approve() => Status = Status.Approved;
    public void Decline() => Status = Status.Declined;
    public void Comment(string comment) => ModeratorComment = comment;
}
