namespace Domain.Test.AggregateModels.ReportAggregateTests.Entities.EvidenceEntityTests;
[TestClass]
public class WhenAddingEvidenceForReport
{
    [TestMethod]
    public void Should_AddEvidence()
    {
        var report = DataProvider.TestReport;
        var authorId = DataProvider.AuthorId;
        var caption = DataProvider.Caption;
        var description = DataProvider.Description;
        var no = DataProvider.No;
        var street1 = DataProvider.Street1;
        var street2 = DataProvider.Street2;
        var city = DataProvider.City;
        var province = DataProvider.Province;
        var mediaItems = Enumerable.Repeat(DataProvider.TestMediaItem, 3).ToList();

        report.AddEvidence(
            authorId,
            caption,
            description,
            no,
            street1,
            street2,
            city,
            province,
            mediaItems);

        Assert.AreEqual(1, report.Evidences.Count);
        Assert.AreEqual(authorId, report.Evidences[0].AuthorId);
        Assert.AreEqual(caption, report.Evidences[0].Caption);
        Assert.AreEqual(description, report.Evidences[0].Description);
        Assert.IsNotNull(report.Evidences[0].CreatedAt);
        Assert.AreEqual(no, report.Evidences[0].Location.No);
        Assert.AreEqual(street1, report.Evidences[0].Location.Street1);
        Assert.AreEqual(street2, report.Evidences[0].Location.Street2);
        Assert.AreEqual(city, report.Evidences[0].Location.City);
        Assert.AreEqual(province, report.Evidences[0].Location.Province);
        Assert.AreEqual(3, report.Evidences[0].MediaItems.Count);
    }

    [TestMethod]
    public void ShouldRaise_EvidenceAddedForReportEvent()
    {
        var report = DataProvider.TestReport;
        var authorId = DataProvider.AuthorId;
        var caption = DataProvider.Caption;
        var description = DataProvider.Description;
        var no = DataProvider.No;
        var street1 = DataProvider.Street1;
        var street2 = DataProvider.Street2;
        var city = DataProvider.City;
        var province = DataProvider.Province;
        var mediaItems = Enumerable.Repeat(DataProvider.TestMediaItem, 3).ToList();

        report.AddEvidence(
            authorId,
            caption,
            description,
            no,
            street1,
            street2,
            city,
            province,
            mediaItems);

        Assert.IsTrue(report.HasDomainEvent<EvidenceAddedForReportEvent>());
    }
}
