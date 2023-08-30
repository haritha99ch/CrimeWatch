using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Domain.Test.AggregateModels;
[TestClass]
public class EvidenceTests
{
    // Arrange
    private readonly WitnessId _authorId = new(new());
    private readonly ReportId _reportId = new(new());
    private readonly string _caption = "Evidence Caption";
    private readonly string _description = "Details about the evidence.";
    private readonly Location _location = Location.Create(null, "456 Elm St", null, "Townsville", "Stateville");
    private readonly List<MediaItem> _mediaItems = new();

    [TestMethod]
    public void EvidenceCreate_ReturnsValidEvidence()
    {
        // Act
        Evidence evidence = Evidence.Create(_authorId, _reportId, _caption, _description, _location, _mediaItems);

        // Assert
        Assert.IsNotNull(evidence);
        Assert.AreEqual(_caption, evidence.Title);
        Assert.AreEqual(_description, evidence.Description);
        Assert.AreEqual(_location, evidence.Location);
        Assert.AreEqual(Status.Pending, evidence.Status);
    }

    [TestMethod]
    public void AddMediaItem_MediaItemCountIncrease()
    {
        // Arrange
        Evidence evidence = Evidence.Create(_authorId, _reportId, _caption, _description, _location, _mediaItems);
        MediaItem _mediaItem = MediaItem.Create(MediaItemType.Image, "https://example.com/image.jpg");

        // Act
        evidence.AddMediaItem(_mediaItem);

        // Assert
        Assert.AreEqual(1, evidence.MediaItems.Count);
    }

    [TestMethod]
    public void ModerateEvidence_StatusChangesToUnderReview()
    {
        // Arrange
        var evidence = Evidence.Create(_authorId, _reportId, _caption, _description, _location, _mediaItems);
        var moderatorId = new ModeratorId(Guid.NewGuid());

        // Act
        evidence.Moderate(moderatorId);

        // Assert
        Assert.AreEqual(moderatorId, evidence.ModeratorId);
        Assert.AreEqual(Status.UnderReview, evidence.Status);
    }

    [TestMethod]
    public void ApproveEvidence_StatusChangesToApproved()
    {
        // Arrange
        var evidence = Evidence.Create(_authorId, _reportId, _caption, _description, _location, _mediaItems);

        // Act
        evidence.Approve();

        // Assert
        Assert.AreEqual(Status.Approved, evidence.Status);
    }

    [TestMethod]
    public void DeclineEvidence_StatusChangesToDeclined()
    {
        // Arrange
        var evidence = Evidence.Create(_authorId, _reportId, _caption, _description, _location, _mediaItems);

        // Act
        evidence.Decline();

        // Assert
        Assert.AreEqual(Status.Declined, evidence.Status);
    }

    [TestMethod]
    public void AddCommentToEvidence_Success()
    {
        // Arrange
        var evidence = Evidence.Create(_authorId, _reportId, _caption, _description, _location, _mediaItems);
        var comment = "This evidence requires further analysis.";

        // Act
        evidence.Comment(comment);

        // Assert
        Assert.AreEqual(comment, evidence.ModeratorComment);
    }
}