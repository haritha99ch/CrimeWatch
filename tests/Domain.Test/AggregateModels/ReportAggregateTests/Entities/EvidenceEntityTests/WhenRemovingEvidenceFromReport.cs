namespace Domain.Test.AggregateModels.ReportAggregateTests.Entities.EvidenceEntityTests;
public class WhenRemovingEvidenceFromReport
{
    [TestMethod]
    public void Should_RemoveEvidenceFromReport()
    {
        var report = DataProvider.TestReportWithAEvidence;

        var deleted = report.RemoveEvidence(report.Evidences[0].Id);

        Assert.IsTrue(deleted);
        Assert.AreEqual(0, report.Evidences.Count);
    }

    [TestMethod]
    public void ShouldRaise_EvidenceFromReportRemovedEvent()
    {
        var report = DataProvider.TestReportWithAEvidence;

        var deleted = report.RemoveEvidence(report.Evidences[0].Id);

        Assert.IsTrue(report.HasDomainEvent<EvidenceFromReportRemovedEvent>());
    }
}
