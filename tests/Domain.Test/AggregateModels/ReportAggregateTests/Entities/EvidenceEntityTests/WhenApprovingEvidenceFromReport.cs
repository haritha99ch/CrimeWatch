namespace Domain.Test.AggregateModels.ReportAggregateTests.Entities.EvidenceEntityTests;

[TestClass]
public class WhenApprovingEvidenceFromReport
{
    [TestMethod]
    public void Should_ApproveEvidence()
    {
        var report = DataProvider.TestReportWithAEvidence;

        report.SetApproveEvidence(report.Evidences[0].Id);

        Assert.AreEqual(Status.Approved, report.Evidences[0].Status);
    }

    [TestMethod]
    public void ShouldRaise_EvidenceFromReportApprovedEvent()
    {
        var report = DataProvider.TestReportWithAEvidence;

        report.SetApproveEvidence(report.Evidences[0].Id);

        Assert.IsTrue(report.HasDomainEvent<EvidenceFromReportApprovedEvent>());
    }
}
