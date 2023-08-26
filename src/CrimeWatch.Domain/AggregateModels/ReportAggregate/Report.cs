namespace CrimeWatch.Domain.AggregateModels.ReportAggregate;
public class Report : AggregateRoot<ReportId>
{
    public WitnessId WitnessId { get; set; } = default!;
    public ModeratorId? ModeratorId { get; set; }
    public string Caption { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime DateTime { get; set; } = DateTime.UtcNow;
    public Location Location { get; set; } = new();
    public Status Status { get; set; } = Status.Pending;
    public List<WitnessId> StaredBy { get; set; } = new();
    public string ModeratorComment { get; set; } = string.Empty;

    public Witness? Witness { get; set; }
    public Moderator? Moderator { get; set; }
    public MediaItem? MediaItem { get; set; }
    public List<Evidence> Evidences { get; set; } = new();

    public static Report Create(
        WitnessId authorId,
        string title,
        string description,
        Location location,
        MediaItem mediaItem
        ) => new()
        {
            Id = new(Guid.NewGuid()),
            WitnessId = authorId,
            Caption = title,
            Description = description,
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
