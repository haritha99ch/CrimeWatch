namespace Domain.Test.AggregateModels.ReportAggregateTests.Entities.CommentEntityTests;

[TestClass]
public class WhenUpdatingCommentOnReport
{
    [TestMethod]
    public void Should_UpdateCommentFromReport()
    {
        var report = DataProvider.TestReportWithAComment;
        var updatedComment = DataProvider.Description;

        report.UpdateComment(report.Comments[0].Id, updatedComment);

        Assert.AreEqual(1, report.Comments.Count);
        Assert.AreEqual(updatedComment, report.Comments[0].Content);
    }

    [TestMethod]
    public void ShouldRaise_CommentFromReportUpdatedEvent()
    {
        var report = DataProvider.TestReportWithAComment;
        var updatedComment = DataProvider.Description;

        report.UpdateComment(report.Comments[0].Id, updatedComment);

        Assert.IsTrue(report.HasDomainEvent<CommentFromReportUpdatedEvent>());
    }
}
