namespace Domain.Test.AggregateModels.ReportAggregateTests.Entities.CommentEntityTests;
[TestClass]
public class WhenUpdatingCommentFromEvidenceOnReport
{
    [TestMethod]
    public void Should_UpdateCommentFromEvidence()
    {
        var report = DataProvider.TestReportWithAEvidenceIncludingComment;
        var updatedComment = DataProvider.Description;

        report.UpdateCommentInEvidence(
            report.Evidences[0].Id,
            report.Evidences[0].Comments[0].Id,
            updatedComment);

        Assert.AreEqual(1, report.Evidences[0].Comments.Count);
        Assert.AreEqual(updatedComment, report.Evidences[0].Comments[0].Content);
    }

    [TestMethod]
    public void ShouldRaise_CommentFromEvidenceOnReportUpdatedEvent()
    {
        var report = DataProvider.TestReportWithAEvidenceIncludingComment;
        var updatedComment = DataProvider.Description;

        report.UpdateCommentInEvidence(
            report.Evidences[0].Id,
            report.Evidences[0].Comments[0].Id,
            updatedComment);

        Assert.IsTrue(report.HasDomainEvent<CommentFromEvidenceOnReportUpdatedEvent>());
    }
}
