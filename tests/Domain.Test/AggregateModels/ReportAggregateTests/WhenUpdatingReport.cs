namespace Domain.Test.AggregateModels.ReportAggregateTests;
[TestClass]
public class WhenUpdatingReport
{
    [TestMethod]
    public void Should_Update()
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

        report.Update(
            updatedCaption,
            updatedDescription,
            no,
            street1,
            updatedStreet2,
            city,
            province,
            violationTypes,
            null,
            newMediaItem);

        Assert.IsNotNull(report.MediaItem);
        Assert.IsNotNull(report.AuthorId);
        Assert.IsNull(report.ModeratorId);
        Assert.AreEqual(updatedCaption, report.Caption);
        Assert.AreEqual(updatedDescription, report.Description);
        Assert.AreEqual(no, report.Location.No);
        Assert.AreEqual(street1, report.Location.Street1);
        Assert.AreEqual(updatedStreet2, report.Location.Street2);
        Assert.AreEqual(city, report.Location.City);
        Assert.AreEqual(province, report.Location.Province);
        Assert.AreEqual(Status.Pending, report.Status);
        Assert.AreSame(violationTypes, report.ViolationTypes);
    }

    [TestMethod]
    public void ShouldRaise_ReportUpdatedEvent()
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

        report.Update(
            updatedCaption,
            updatedDescription,
            no,
            street1,
            updatedStreet2,
            city,
            province,
            violationTypes,
            null,
            newMediaItem);

        Assert.IsTrue(report.HasDomainEvent<ReportUpdatedEvent>());
    }
}
