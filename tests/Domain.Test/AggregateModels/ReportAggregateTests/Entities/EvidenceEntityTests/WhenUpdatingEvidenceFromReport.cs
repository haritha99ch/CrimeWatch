namespace Domain.Test.AggregateModels.ReportAggregateTests.Entities.EvidenceEntityTests;
[TestClass]
public class WhenUpdatingEvidenceFromReport
{
    [TestMethod]
    public void Should_UpdateEvidence()
    {
        var report = DataProvider.TestReportWithAEvidence;
        var updatedCaption = DataProvider.Caption;
        var updatedDescription = DataProvider.Description;
        var no = report.Location.No!;
        var street1 = report.Location.Street1;
        var updatedStreet2 = DataProvider.Street2;
        var city = report.Location.City;
        var province = report.Location.Province;
        var newMediaItems = Enumerable.Repeat(DataProvider.TestMediaItem, 3).ToList();
        var exitingMediaItems = report.Evidences[0].MediaItems;
        exitingMediaItems.RemoveAt(1);

        report.UpdateEvidence(
            report.Evidences[0].Id,
            updatedCaption,
            updatedDescription,
            no,
            street1,
            updatedStreet2,
            city,
            province,
            exitingMediaItems,
            newMediaItems);

        Assert.IsNotNull(report.MediaItem);
        Assert.IsNotNull(report.AuthorId);
        Assert.IsNull(report.ModeratorId);
        Assert.AreEqual(updatedCaption, report.Evidences[0].Caption);
        Assert.AreEqual(updatedDescription, report.Evidences[0].Description);
        Assert.AreEqual(no, report.Evidences[0].Location.No);
    }

    [TestMethod]
    public void ShouldRaise_EvidenceFromReportUpdatedEvent()
    {
        var report = DataProvider.TestReportWithAEvidence;
        var updatedCaption = DataProvider.Caption;
        var updatedDescription = DataProvider.Description;
        var no = report.Location.No!;
        var street1 = report.Location.Street1;
        var updatedStreet2 = DataProvider.Street2;
        var city = report.Location.City;
        var province = report.Location.Province;
        var newMediaItems = Enumerable.Repeat(DataProvider.TestMediaItem, 3).ToList();
        var exitingMediaItems = report.Evidences[0].MediaItems;
        exitingMediaItems.RemoveAt(1);

        report.UpdateEvidence(
            report.Evidences[0].Id,
            updatedCaption,
            updatedDescription,
            no,
            street1,
            updatedStreet2,
            city,
            province,
            exitingMediaItems,
            newMediaItems);

        Assert.IsTrue(report.HasDomainEvent<EvidenceFromReportUpdatedEvent>());
    }
}
