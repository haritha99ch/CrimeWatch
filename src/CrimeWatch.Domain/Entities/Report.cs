using CrimeWatch.Domain.Models;

namespace CrimeWatch.Domain.Entities;
public class Report : Entity<ReportId>
{
    public WitnessId WitnessId { get; set; } = default!;
    public ModeratorId? ModeratorId { get; set; }
    public string Title { get; set; } = string.Empty;
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
            WitnessId witnessId,
            string title,
            string description,
            Location location,
            MediaItem mediaItem,
            List<Evidence>? evidences = null
        ) => new()
    {
        Id = new(Guid.NewGuid()),
        WitnessId = witnessId,
        Title = title,
        Description = description,
        Location = location,
        MediaItem = mediaItem,
        Evidences = evidences ?? new List<Evidence>()
    };

    public void AddEvidence(Evidence evidence) => Evidences.Add(evidence);
    public void Moderate(ModeratorId moderatorId)
    {
        ModeratorId = moderatorId;
        Status = Status.UnderReview;
    }

    public void Approve() => Status = Status.Approved;
    public void Decline() => Status = Status.Declined;
    public void RevertToReview() => Status = Status.UnderReview;
    public void Comment(string comment) => ModeratorComment = comment;

    public Report Update(string title, string description, Location location, MediaItem mediaItem)
    {
        Title = title;
        Description = description;
        Location = location;
        MediaItem = mediaItem;
        return this;
    }
}
