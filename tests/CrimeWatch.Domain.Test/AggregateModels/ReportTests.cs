using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Domain.Test.AggregateModels;
[TestClass]
public class ReportTests
{
    [TestMethod]
    public void ReportCreate_ReturnsValidReport()
    {
        // Arrange
        WitnessId authorId = new(new());
        string title = "Suspicious Activity";
        string description = "Report of suspicious activity in the neighborhood.";
        DateTime date = DateTime.UtcNow;
        Location location = Location.Create(null, "123 Main St", null, "Cityville", "Stateville");
        MediaItem mediaItem = MediaItem.CreateForReport(MediaItemType.Image, "https://example.com/image.jpg", new(new()));

        // Act
        Report report = Report.Create(authorId, title, description, date, location, mediaItem);

        // Assert
        Assert.IsNotNull(report);
        Assert.AreEqual(title, report.Title);
        Assert.AreEqual(description, report.Description);
        Assert.AreEqual(date, report.Date);
        Assert.AreEqual(location, report.Location);
        Assert.AreEqual(mediaItem, report.MediaItem);
        Assert.AreEqual(Status.Pending, report.Status);
    }
}