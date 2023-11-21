namespace Domain.Test.AggregateModels.ReportAggregateTests.Entities.EvidenceEntityTests;

[TestClass]
public class WhenModeratingEvidenceFromReport
{
    [TestMethod]
    public void Should_ModerateEvidence()
    {
        var report = DataProvider.TestReportWithAEvidence;
        var moderatorId = DataProvider.ModeratorId;

        report.SetModeratorModeratorForEvidence(report.Evidences[0].Id, moderatorId);

        Assert.IsNotNull(report.Evidences[0].ModeratorId);
        Assert.AreEqual(moderatorId, report.Evidences[0].ModeratorId);
        Assert.AreEqual(Status.UnderReview, report.Evidences[0].Status);
    }

    [TestMethod]
    public void ShouldRaise_EvidenceFromReportModeratedEvent()
    {
        var report = DataProvider.TestReportWithAEvidence;
        var moderatorId = DataProvider.ModeratorId;

        report.SetModeratorModeratorForEvidence(report.Evidences[0].Id, moderatorId);

        Assert.IsTrue(report.HasDomainEvent<EvidenceFromReportModeratedEvent>());
    }
}
