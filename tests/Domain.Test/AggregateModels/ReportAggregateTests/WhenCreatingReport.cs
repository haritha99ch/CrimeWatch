namespace Domain.Test.AggregateModels.ReportAggregateTests;
[TestClass]
public class WhenCreatingReport
{
    [TestMethod]
    public void Should_Create()
    {
        var caption = DataProvider.Caption;
        var description = DataProvider.Description;
        var no = DataProvider.No;
        var street1 = DataProvider.Street1;
        var street2 = DataProvider.Street2;
        var city = DataProvider.City;
        var province = DataProvider.Province;
        var authorId = DataProvider.AuthorId;
        var mediaItemUpload = DataProvider.TestMediaItem;
        var violationTypes = DataProvider.ViolationTypes;

        var report = Report.Create(authorId, caption, description, no, street1, street2, city, province, violationTypes,
            mediaItemUpload);

        Assert.IsNotNull(report);
        Assert.IsNotNull(report.MediaItem);
        Assert.AreEqual(authorId, report.AuthorId);
        Assert.AreEqual(caption, report.Caption);
        Assert.AreEqual(description, report.Description);
        Assert.AreEqual(no, report.Location.No);
        Assert.AreEqual(street1, report.Location.Street1);
        Assert.AreEqual(street2, report.Location.Street2);
        Assert.AreEqual(city, report.Location.City);
        Assert.AreEqual(province, report.Location.Province);
        Assert.AreEqual(violationTypes, report.ViolationTypes);
    }

    [TestMethod]
    public void ShouldRaise_ReportCreatedEvent()
    {
        var report = DataProvider.TestReport;
        var updatedCaption = DataProvider.Caption;
        var updatedDescription = DataProvider.Description;
        var no = report.Location.No!;
        var street1 = report.Location.Street1;
        var updatedStreet2 = DataProvider.Street2;
        var city = report.Location.City;
        var province = report.Location.Province;
        var newMediaItem = DataProvider.TestMediaItem;
        var violationTypes = DataProvider.ViolationTypes;

        report.Update(updatedCaption, updatedDescription, no, street1, updatedStreet2, city, province, violationTypes,
            null, newMediaItem);

        Assert.IsTrue(report.HasDomainEvent<ReportUpdatedEvent>());
    }
}
