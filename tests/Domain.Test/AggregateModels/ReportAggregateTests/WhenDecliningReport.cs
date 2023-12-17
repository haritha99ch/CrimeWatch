namespace Domain.Test.AggregateModels.ReportAggregateTests;
public class WhenDecliningReport
{
    [TestMethod]
    public void Should_Decline()
    {
        var report = DataProvider.TestModeratedReport;

        report.SetDeclined();

        Assert.AreEqual(Status.Declined, report.Status);
        Assert.IsNotNull(report.UpdatedAt);
    }

    [TestMethod]
    public void ShouldRaise_ReportDeclinedEvent()
    {
        var report = DataProvider.TestModeratedReport;

        report.SetDeclined();

        Assert.IsTrue(report.HasDomainEvent<ReportDeclinedEvent>());
    }
}
