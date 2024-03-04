using Application.Errors.Common;
using Application.Features.Reports.Commands.ApproveReport;
using Application.Features.Reports.Commands.RevertReportToUnderReview;

namespace Application.Test.Features.Reports.Commands;
[TestClass]
public class WhenRevertingReportToUnderReview : TestBase
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
    public async Task ShouldReturnTrue_When_CurrentUser_Is_ReviewingTheReport()
    {
        var testModerator = DataProvider.TestAccountForModerator;
        var testWitness = DataProvider.TestAccountForWitness;
        var testReport = DataProvider.GetReport(testWitness.Id);
        testReport.SetModerator(testModerator.Id);
        testReport.SetApproved();
        await DbContext.Accounts.AddRangeAsync([testModerator, testWitness]);
        await DbContext.Reports.AddAsync(testReport);
        await SaveAndClearChangeTrackerAsync();
        GenerateTokenAndInvoke(testModerator);

        var command = new RevertReportToUnderReviewCommand(testReport.Id);
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
        testReport.SetApproved();
        await DbContext.Accounts.AddRangeAsync([testModerator, testWitness]);
        await DbContext.Reports.AddAsync(testReport);
        await SaveAndClearChangeTrackerAsync();
        GenerateTokenAndInvoke(testWitness);

        var command = new RevertReportToUnderReviewCommand(testReport.Id);
        var result = await Mediator.Send(command);

        Assert.IsTrue(result.GetError().Is<UnauthorizedError>());
    }

    [TestMethod]
    public async Task ShouldReturn_ReportIsUnderReviewError_When_Report_IsNot_UnderReview()
    {
        var testModerator = DataProvider.TestAccountForModerator;
        var testWitness = DataProvider.TestAccountForWitness;
        var testReport = DataProvider.GetReport(testWitness.Id);
        testReport.SetModerator(testModerator.Id);
        await DbContext.Accounts.AddRangeAsync([testModerator, testWitness]);
        await DbContext.Reports.AddAsync(testReport);
        await SaveAndClearChangeTrackerAsync();
        GenerateTokenAndInvoke(testModerator);

        var command = new RevertReportToUnderReviewCommand(testReport.Id);
        var result = await Mediator.Send(command);

        Assert.IsTrue(result.GetError().Is<ReportIsUnderReviewError>());
    }

    [TestMethod]
    public async Task ShouldReturn_UnauthorizedError_When_CurrentUser_IsNot_Reviewing()
    {
        var testModerator = DataProvider.TestAccountForModerator;
        var currentModerator = DataProvider.TestAccountForModerator;
        var testWitness = DataProvider.TestAccountForWitness;
        var testReport = DataProvider.GetReport(testWitness.Id);
        testReport.SetModerator(testModerator.Id);
        testReport.SetApproved();
        await DbContext.Accounts.AddRangeAsync([testModerator, testWitness, currentModerator]);
        await DbContext.Reports.AddAsync(testReport);
        await SaveAndClearChangeTrackerAsync();
        GenerateTokenAndInvoke(currentModerator);

        var command = new RevertReportToUnderReviewCommand(testReport.Id);
        var result = await Mediator.Send(command);

        Assert.IsTrue(result.GetError().Is<UnauthorizedError>());
    }
    
}
