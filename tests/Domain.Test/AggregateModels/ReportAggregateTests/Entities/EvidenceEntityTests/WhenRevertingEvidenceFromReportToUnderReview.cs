namespace Domain.Test.AggregateModels.ReportAggregateTests.Entities.EvidenceEntityTests;

[TestClass]
public class WhenRevertingEvidenceFromReportToUnderReview
{
    [TestMethod]
    public void Should_RevertEvidenceToUnderReview()
    {
        var report = DataProvider.TestReportWithAEvidence;

        report.SetUnderReviewEvidence(report.Evidences[0].Id);

        Assert.AreEqual(Status.UnderReview, report.Evidences[0].Status);
    }

    [TestMethod]
    public void ShouldRaise_EvidenceFromReportRevertedToUnderReviewEvent()
    {
        var report = DataProvider.TestReportWithAEvidence;

        report.SetUnderReviewEvidence(report.Evidences[0].Id);

        Assert.IsTrue(report.HasDomainEvent<EvidenceFromReportRevertedToUnderReviewEvent>());
    }
}
