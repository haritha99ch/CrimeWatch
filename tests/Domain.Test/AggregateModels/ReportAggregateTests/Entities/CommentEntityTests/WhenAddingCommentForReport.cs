namespace Domain.Test.AggregateModels.ReportAggregateTests.Entities.CommentEntityTests;
[TestClass]
public class WhenAddingCommentForReport
{
    [TestMethod]
    public void Should_AddCommentForReport()
    {
        var report = DataProvider.TestReport;
        var comment = DataProvider.Description;
        var authorId = DataProvider.AuthorId;

        report.AddComment(authorId, comment);

        Assert.AreEqual(report.Comments.Count, 1);
        Assert.AreEqual(report.Comments[0].AuthorId, authorId);
        Assert.AreEqual(report.Comments[0].Content, comment);
        Assert.IsNotNull(report.Comments[0].CreatedAt);
    }

    [TestMethod]
    public void ShouldRaise_CommentAddedForReportEvent()
    {
        var report = DataProvider.TestReport;
        var comment = DataProvider.Description;
        var authorId = DataProvider.AuthorId;

        report.AddComment(authorId, comment);

        Assert.IsTrue(report.HasDomainEvent<CommentForReportAddedEvent>());
    }
}
