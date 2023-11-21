namespace Domain.Test.AggregateModels.ReportAggregateTests.Entities.BookmarkEntityTests;

[TestClass]
public class WhenRemovingReportBookmark
{
    [TestMethod]
    public void Should_RemoveReportBookmark()
    {
        var report = DataProvider.TestReportWithABookmark;
        var accountId = report.Bookmarks[0].AccountId;

        report.RemoveBookmark(accountId);

        Assert.AreEqual(0, report.Bookmarks.Count);
    }

    [TestMethod]
    public void ShouldRaise_ReportBookmarkRemovedEvent()
    {
        var report = DataProvider.TestReportWithABookmark;
        var accountId = report.Bookmarks[0].AccountId;

        report.RemoveBookmark(accountId);

        Assert.IsTrue(report.HasDomainEvent<ReportBookmarkRemovedEvent>());
    }
}
