using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Domain.Test.AggregateModels;
[TestClass]
public class ReportTests
{
    // Arrange
    private readonly WitnessId _authorId = new(new());
    private readonly string _title = "Suspicious Activity";
    private readonly string _description = "Report of suspicious activity in the neighborhood.";
    private readonly Location _location = Location.Create(null, "123 Main St", null, "Cityville", "Stateville");
    private readonly MediaItem _mediaItem = MediaItem.Create(MediaItemType.Image, "https://example.com/image.jpg");

    [TestMethod]
    public void ReportCreate_ReturnsValidReport()
    {
        // Act
        Report report = Report.Create(_authorId, _title, _description, _location, _mediaItem);

        // Assert
        Assert.IsNotNull(report);
        Assert.AreEqual(_title, report.Title);
        Assert.AreEqual(_description, report.Description);
        Assert.AreEqual(_location, report.Location);
        Assert.AreEqual(_mediaItem, report.MediaItem);
        Assert.AreEqual(Status.Pending, report.Status);
    }

    [TestMethod]
    public void AddEvidenceToReport_Success()
    {
        // Arrange
        var report = Report.Create(_authorId, _title, _description, _location, _mediaItem);
        WitnessId authorId = new(new());
        ReportId reportId = new(new());
        string caption = "Evidence Caption";
        string description = "Details about the evidence.";
        Location location = Location.Create(null, "456 Elm St", null, "Townsville", "Stateville");
        List<MediaItem> mediaItems = new();

        // Act
        Evidence evidence = Evidence.Create(authorId, reportId, caption, description, location, mediaItems);

        // Act
        report.AddEvidence(evidence);

        // Assert
        Assert.AreEqual(1, report.Evidences.Count);
        Assert.AreEqual(evidence.Id, report.Evidences[0].Id);
    }

    [TestMethod]
    public void ModerateReport_StatusChangesToUnderReview()
    {
        // Arrange
        var report = Report.Create(_authorId, _title, _description, _location, _mediaItem);
        var moderatorId = new ModeratorId(Guid.NewGuid());

        // Act
        report.Moderate(moderatorId);

        // Assert
        Assert.AreEqual(moderatorId, report.ModeratorId);
        Assert.AreEqual(Status.UnderReview, report.Status);
    }

    [TestMethod]
    public void ApproveReport_StatusChangesToApproved()
    {
        // Arrange
        var report = Report.Create(_authorId, _title, _description, _location, _mediaItem);

        // Act
        report.Approve();

        // Assert
        Assert.AreEqual(Status.Approved, report.Status);
    }

    [TestMethod]
    public void DeclineReport_StatusChangesToDeclined()
    {
        // Arrange
        var report = Report.Create(_authorId, _title, _description, _location, _mediaItem);

        // Act
        report.Decline();

        // Assert
        Assert.AreEqual(Status.Declined, report.Status);
    }

    [TestMethod]
    public void AddCommentToReport_Success()
    {
        // Arrange
        var report = Report.Create(_authorId, _title, _description, _location, _mediaItem);
        var comment = "This report requires further investigation.";

        // Act
        report.Comment(comment);

        // Assert
        Assert.AreEqual(comment, report.ModeratorComment);
    }
}