namespace Domain.Test.AggregateModels.ReportAggregateTests.Entities.CommentEntityTests;

[TestClass]
public class WhenRemovingCommentOnReport
{
    [TestMethod]
    public void Should_RemoveCommentFromReport()
    {
        var report = DataProvider.TestReportWithAComment;

        var deleted = report.RemoveComment(report.Comments[0].Id);

        Assert.IsTrue(deleted);
        Assert.AreEqual(0, report.Comments.Count);
    }

    [TestMethod]
    public void ShouldRaise_CommentFromReportRemovedEvent()
    {
        var report = DataProvider.TestReportWithAComment;

        report.RemoveComment(report.Comments[0].Id);

        Assert.IsTrue(report.HasDomainEvent<CommentFromReportRemovedEvent>());
    }
}
