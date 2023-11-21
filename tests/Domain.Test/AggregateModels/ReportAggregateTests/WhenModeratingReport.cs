namespace Domain.Test.AggregateModels.ReportAggregateTests;

[TestClass]
public class WhenModeratingReport
{
    [TestMethod]
    public void Should_Moderate()
    {
        var report = DataProvider.TestReport;
        var moderatorId = DataProvider.ModeratorId;

        report.SetModerator(moderatorId);

        Assert.IsNotNull(report.ModeratorId);
        Assert.AreEqual(moderatorId, report.ModeratorId);
        Assert.AreEqual(Status.UnderReview, report.Status);
        Assert.IsNotNull(report.UpdatedAt);
    }

    [TestMethod]
    public void ShouldRaise_ReportModeratedEvent()
    {
        var report = DataProvider.TestReport;
        var moderatorId = DataProvider.ModeratorId;

        report.SetModerator(moderatorId);

        Assert.IsTrue(report.HasDomainEvent<ReportModeratedEvent>());
    }
}
