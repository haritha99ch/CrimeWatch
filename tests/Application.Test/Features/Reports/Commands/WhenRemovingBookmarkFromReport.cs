using Application.Features.Reports.Commands.RemoveBookmark;

namespace Application.Test.Features.Reports.Commands;
[TestClass]
public class WhenRemovingBookmarkFromReport : TestBase
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
    public async Task ShouldRemoveBookmark_When_UserIsAlready_Bookmarked()
    {
        var bookmarkedAccounts = Enumerable.Range(1, 5).Select(_ => DataProvider.TestAccountForWitness).ToList();
        var testAccount = DataProvider.TestAccountForWitness;
        var testReport = DataProvider.GetReport(testAccount.Id);
        bookmarkedAccounts.Add(testAccount);
        bookmarkedAccounts.ForEach(e => testReport.AddBookmark(e.Id));
        testReport.SetApproved();
        await DbContext.Accounts.AddRangeAsync(bookmarkedAccounts);
        await DbContext.Reports.AddAsync(testReport);
        await SaveAndClearChangeTrackerAsync();
        GenerateTokenAndInvoke(testAccount);

        var command = new RemoveBookmarkCommand(testReport.Id, testAccount.Id);
        var result = await Mediator.Send(command);

        Assert.IsTrue(result.GetValue());
    }
}
