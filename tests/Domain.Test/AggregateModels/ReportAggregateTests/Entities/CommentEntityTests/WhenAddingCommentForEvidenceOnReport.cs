namespace Domain.Test.AggregateModels.ReportAggregateTests.Entities.CommentEntityTests;

[TestClass]
public class WhenAddingCommentForEvidenceOnReport
{
    [TestMethod]
    public void Should_AddCommentForEvidence()
    {
        var report = DataProvider.TestReportWithAEvidence;
        var comment = DataProvider.Description;
        var authorId = DataProvider.AuthorId;

        report.AddCommentToEvidence(report.Evidences[0].Id, authorId, comment);

        Assert.AreEqual(1, report.Evidences[0].Comments.Count);
        Assert.AreEqual(authorId, report.Evidences[0].Comments[0].AuthorId);
        Assert.AreEqual(comment, report.Evidences[0].Comments[0].Content);
        Assert.IsNotNull(report.Evidences[0].Comments[0].CreatedAt);
    }

    [TestMethod]
    public void ShouldRaise_CommentForEvidenceOnReportAddedEvent()
    {
        var report = DataProvider.TestReportWithAEvidence;
        var comment = DataProvider.Description;
        var authorId = DataProvider.AuthorId;

        report.AddCommentToEvidence(report.Evidences[0].Id, authorId, comment);

        Assert.IsTrue(report.HasDomainEvent<CommentForEvidenceOnReportAddedEvent>());
    }
}
