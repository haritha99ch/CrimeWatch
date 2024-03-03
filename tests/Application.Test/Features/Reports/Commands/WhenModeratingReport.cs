using Application.Features.Reports.Commands.ModerateReport;

namespace Application.Test.Features.Reports.Commands;
[TestClass]
public class WhenModeratingReport : TestBase
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
    public async Task ShouldModerate_When_ReportIs_Pending()
    {
        var testWitness = DataProvider.TestAccountForWitness;
        var testReport = DataProvider.GetReport(testWitness.Id);
        var testModerator = DataProvider.TestAccountForModerator;
        await DbContext.Accounts.AddRangeAsync([testModerator, testWitness]);
        await DbContext.Reports.AddAsync(testReport);
        await SaveAndClearChangeTrackerAsync();
        GenerateTokenAndInvoke(testModerator);

        var command = new ModerateReportCommand(testReport.Id, testModerator.Id);
        var result = await Mediator.Send(command);

        Assert.IsTrue(result.GetValue());
    }

}
