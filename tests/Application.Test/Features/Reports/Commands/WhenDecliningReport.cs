using Application.Errors.Common;
using Application.Features.Reports.Commands.ApproveReport;
using Application.Features.Reports.Commands.DeclineReport;

namespace Application.Test.Features.Reports.Commands;
[TestClass]
public class WhenDecliningReport : TestBase
{
    [TestInitialize]
    public async Task Initialized()
    {
        await InitializeAsync();
    }

    [TestCleanup]
    public async Task Cleanup()
    {
        await CleanupAsync();
    }
    
        [TestMethod]
    public async Task ShouldReturnTrue_When_CurrentUser_Is_ReviewingTheReport()
    {
        var testModerator = DataProvider.TestAccountForModerator;
        var testWitness = DataProvider.TestAccountForWitness;
        var testReport = DataProvider.GetReport(testWitness.Id);
        testReport.SetModerator(testModerator.Id);
        await DbContext.Accounts.AddRangeAsync([testModerator, testWitness]);
        await DbContext.Reports.AddAsync(testReport);
        await SaveAndClearChangeTrackerAsync();
        GenerateTokenAndInvoke(testModerator);

        var command = new DeclineReportCommand(testReport.Id);
        var result = await Mediator.Send(command);

        Assert.IsTrue(result.GetValue());
    }

    [TestMethod]
    public async Task ShouldReturn_UnauthorizedError_When_CurrentUser_IsAWitness()
    {
        var testModerator = DataProvider.TestAccountForModerator;
        var testWitness = DataProvider.TestAccountForWitness;
        var testReport = DataProvider.GetReport(testWitness.Id);
        testReport.SetModerator(testModerator.Id);
        await DbContext.Accounts.AddRangeAsync([testModerator, testWitness]);
        await DbContext.Reports.AddAsync(testReport);
        await SaveAndClearChangeTrackerAsync();
        GenerateTokenAndInvoke(testWitness);

        var command = new DeclineReportCommand(testReport.Id);
        var result = await Mediator.Send(command);

        Assert.IsTrue(result.GetError().Is<UnauthorizedError>());
    }

    [TestMethod]
    public async Task ShouldReturn_ReportIsNotUnderReviewError_When_Report_IsNot_UnderReview()
    {
        var testModerator = DataProvider.TestAccountForModerator;
        var testWitness = DataProvider.TestAccountForWitness;
        var testReport = DataProvider.GetReport(testWitness.Id);
        await DbContext.Accounts.AddRangeAsync([testModerator, testWitness]);
        await DbContext.Reports.AddAsync(testReport);
        await SaveAndClearChangeTrackerAsync();
        GenerateTokenAndInvoke(testModerator);

        var command = new DeclineReportCommand(testReport.Id);
        var result = await Mediator.Send(command);

        Assert.IsTrue(result.GetError().Is<ReportIsNoUnderReviewError>());
    }

    [TestMethod]
    public async Task ShouldReturn_UnauthorizedError_When_CurrentUser_IsNot_Reviewing()
    {
        var testModerator = DataProvider.TestAccountForModerator;
        var currentModerator = DataProvider.TestAccountForModerator;
        var testWitness = DataProvider.TestAccountForWitness;
        var testReport = DataProvider.GetReport(testWitness.Id);
        testReport.SetModerator(testModerator.Id);
        await DbContext.Accounts.AddRangeAsync([testModerator, testWitness, currentModerator]);
        await DbContext.Reports.AddAsync(testReport);
        await SaveAndClearChangeTrackerAsync();
        GenerateTokenAndInvoke(currentModerator);

        var command = new DeclineReportCommand(testReport.Id);
        var result = await Mediator.Send(command);

        Assert.IsTrue(result.GetError().Is<UnauthorizedError>());
    }
}
