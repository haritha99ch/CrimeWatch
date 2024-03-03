using Application.Features.Reports.Commands.BookmarkReport;
using Microsoft.EntityFrameworkCore;

namespace Application.Test.Features.Reports.Commands;
[TestClass]
public class WhenBookmarkingReport : TestBase
{
    [TestInitialize]
    public async Task Initialize()
    {
        await InitializeAsync();
    }

    [TestCleanup]
    public async Task Cleanup()
    {
        await CleanupAsync();
    }

    [TestMethod]
    public async Task ShouldBookmark_When_UserIs_LoggedIn()
    {
        var bookmarkedAccounts = Enumerable.Range(1, 5).Select(_ => DataProvider.TestAccountForWitness).ToList();
        var testAccount = DataProvider.TestAccountForWitness;
        var testReport = DataProvider.GetReport(testAccount.Id);
        bookmarkedAccounts.ForEach(e => testReport.AddBookmark(e.Id));
        testReport.SetApproved();
        bookmarkedAccounts.Add(testAccount);
        await DbContext.Accounts.AddRangeAsync(bookmarkedAccounts);
        await DbContext.Reports.AddAsync(testReport);
        await SaveAndClearChangeTrackerAsync();
        GenerateTokenAndInvoke(testAccount);

        var command = new BookmarkReportCommand(testReport.Id, testAccount.Id);
        var result = await Mediator.Send(command);

        Assert.IsTrue(result.GetValue());
        var report = await DbContext.Reports.Include(e => e.Bookmarks)
            .FirstOrDefaultAsync(e => e.Id.Equals(testReport.Id));
        Assert.AreEqual(bookmarkedAccounts.Count, report!.Bookmarks.Count);
        Assert.AreEqual(bookmarkedAccounts.Count, report!.BookmarksCount);
    }

    [TestMethod]
    public async Task ShouldReturn_AlreadyBookmarkedError_When_UserIs_AlreadyBookmarked()
    {
        var bookmarkedAccounts = Enumerable.Range(1, 5).Select(_ => DataProvider.TestAccountForWitness).ToList();
        var testAccount = DataProvider.TestAccountForWitness;
        var testReport = DataProvider.GetReport(testAccount.Id);
        bookmarkedAccounts.Add(testAccount); // Bookmark as current user.
        bookmarkedAccounts.ForEach(e => testReport.AddBookmark(e.Id));
        testReport.SetApproved();
        await DbContext.Accounts.AddRangeAsync(bookmarkedAccounts);
        await DbContext.Reports.AddAsync(testReport);
        await SaveAndClearChangeTrackerAsync();
        GenerateTokenAndInvoke(testAccount);

        var command = new BookmarkReportCommand(testReport.Id, testAccount.Id);
        var result = await Mediator.Send(command);

        Assert.IsTrue(result.GetError().Is<ReportAlreadyBookmarkError>());
    }
}
