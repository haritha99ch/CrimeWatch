namespace Domain.Test.AggregateModels.ReportAggregateTests.Entities.EvidenceEntityTests;
[TestClass]
public class WhenDecliningEvidenceFromReport
{
    [TestMethod]
    public void Should_DeclineEvidence()
    {
        var report = DataProvider.TestReportWithAEvidence;

        report.SetDeclineEvidence(report.Evidences[0].Id);

        Assert.AreEqual(Status.Declined, report.Evidences[0].Status);
    }

    [TestMethod]
    public void ShouldRaise_EvidenceFromReportDeclinedEvent()
    {
        var report = DataProvider.TestReportWithAEvidence;

        report.SetDeclineEvidence(report.Evidences[0].Id);

        Assert.IsTrue(report.HasDomainEvent<EvidenceFromReportDeclinedEvent>());
    }
}
