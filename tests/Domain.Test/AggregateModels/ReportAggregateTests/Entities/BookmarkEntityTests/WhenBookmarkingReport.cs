namespace Domain.Test.AggregateModels.ReportAggregateTests.Entities.BookmarkEntityTests;
[TestClass]
public class WhenBookmarkingReport
{
    [TestMethod]
    public void Should_Bookmark()
    {
        var report = DataProvider.TestReport;
        var accountId = DataProvider.AccountId;

        report.AddBookmark(accountId);

        Assert.AreEqual(1, report.Bookmarks.Count);
        Assert.AreEqual(accountId, report.Bookmarks[0].AccountId);
        Assert.IsNotNull(report.Bookmarks[0].CreatedAt);
    }

    [TestMethod]
    public void ShouldRaise_ReportBookmarkedEvent()
    {
        var report = DataProvider.TestReport;
        var accountId = DataProvider.AccountId;

        report.AddBookmark(accountId);

        Assert.IsTrue(report.HasDomainEvent<ReportBookmarkedEvent>());
    }
}
