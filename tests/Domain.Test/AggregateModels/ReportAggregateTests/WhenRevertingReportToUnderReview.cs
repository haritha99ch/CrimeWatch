namespace Domain.Test.AggregateModels.ReportAggregateTests;
[TestClass]
public class WhenRevertingReportToUnderReview
{
    [TestMethod]
    public void Should_RevertedToUnderReview()
    {
        var report = DataProvider.TestApprovedReport;

        report.SetUnderReview();

        Assert.AreEqual(Status.UnderReview, report.Status);
        Assert.IsNotNull(report.UpdatedAt);
    }

    [TestMethod]
    public void ShouldRaise_ReportRevertedToUnderReviewEvent()
    {
        var report = DataProvider.TestApprovedReport;

        report.SetUnderReview();

        Assert.IsTrue(report.HasDomainEvent<ReportRevertedToUnderReviewEvent>());
    }
}
