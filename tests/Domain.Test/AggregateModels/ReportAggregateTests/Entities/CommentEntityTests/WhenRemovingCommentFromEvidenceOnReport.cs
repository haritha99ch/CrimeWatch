namespace Domain.Test.AggregateModels.ReportAggregateTests.Entities.CommentEntityTests;

[TestClass]
public class WhenRemovingCommentFromEvidenceOnReport
{
    [TestMethod]
    public void Should_RemoveCommentFromEvidence()
    {
        var report = DataProvider.TestReportWithAEvidenceIncludingComment;

        var deleted = report.RemoveCommentInEvidence(
            report.Evidences[0].Id,
            report.Evidences[0].Comments[0].Id
        );

        Assert.IsTrue(deleted);
        Assert.AreEqual(0, report.Evidences[0].Comments.Count);
    }

    [TestMethod]
    public void ShouldRaise_CommentFromEvidenceOnReportRemovedEvent()
    {
        var report = DataProvider.TestReportWithAEvidenceIncludingComment;

        report.RemoveCommentInEvidence(report.Evidences[0].Id, report.Evidences[0].Comments[0].Id);

        Assert.IsTrue(report.HasDomainEvent<CommentFromEvidenceOnReportRemovedEvent>());
    }
}
