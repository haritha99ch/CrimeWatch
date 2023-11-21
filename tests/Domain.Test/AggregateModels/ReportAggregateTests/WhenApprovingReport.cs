namespace Domain.Test.AggregateModels.ReportAggregateTests;

[TestClass]
public class WhenApprovingReport
{
    [TestMethod]
    public void Should_Approve()
    {
        var report = DataProvider.TestModeratedReport;

        report.SetApproved();

        Assert.AreEqual(Status.Approved, report.Status);
        Assert.IsNotNull(report.UpdatedAt);
    }

    [TestMethod]
    public void ShouldRaise_ReportApprovedEvent()
    {
        var report = DataProvider.TestModeratedReport;

        report.SetApproved();

        Assert.IsTrue(report.HasDomainEvent<ReportApprovedEvent>());
    }
}
