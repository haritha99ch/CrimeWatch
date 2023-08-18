using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Domain.Test.AggregateModels;
[TestClass]
public class EvidenceTests
{
    [TestMethod]
    public void EvidenceCreate_ReturnsValidEvidence()
    {
        // Arrange
        WitnessId authorId = new(new());
        ReportId reportId = new(new());
        string caption = "Evidence Caption";
        string description = "Details about the evidence.";
        DateTime dateTime = DateTime.UtcNow;
        Location location = Location.Create(null, "456 Elm St", null, "Townsville", "Stateville");
        List<MediaItem> mediaItems = new();

        // Act
        Evidence evidence = Evidence.Create(authorId, reportId, caption, description, dateTime, location, mediaItems);

        // Assert
        Assert.IsNotNull(evidence);
        Assert.AreEqual(caption, evidence.Caption);
        Assert.AreEqual(description, evidence.Description);
        Assert.AreEqual(dateTime, evidence.DateTime);
        Assert.AreEqual(location, evidence.Location);
        Assert.AreEqual(Status.Pending, evidence.Status);
    }
}