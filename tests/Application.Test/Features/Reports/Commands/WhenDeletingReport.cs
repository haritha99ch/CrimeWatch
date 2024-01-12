using Application.Errors.Common;
using Application.Features.Reports.Commands.DeleteReport;
using Domain.AggregateModels.ReportAggregate.ValueObjects;

namespace Application.Test.Features.Reports.Commands;
[TestClass]
public class WhenDeletingReport : TestBase
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
    public async Task Should_Delete_Report()
    {
        var testAccount = DataProvider.TestAccountForWitness;
        var testReport = DataProvider.GetReport(testAccount.Id);
        await DbContext.Accounts.AddAsync(testAccount);
        await DbContext.Reports.AddAsync(testReport);
        await DbContext.SaveChangesAsync();
        GenerateTokenAndInvoke(testAccount);
        var command = new DeleteReportCommand(testReport.Id);
        var result = await Mediator.Send(command);

        var value = result.GetValue();
        Assert.IsTrue(value);
    }

    [TestMethod]
    public async Task Should_Return_ReportNotFoundError_When_Report_Is_Not_Found()
    {
        var testAccount = DataProvider.TestAccountForWitness;
        await DbContext.Accounts.AddAsync(testAccount);
        await DbContext.SaveChangesAsync();
        var reportId = new ReportId(Guid.NewGuid());
        GenerateTokenAndInvoke(testAccount);

        var command = new DeleteReportCommand(reportId);
        var result = await Mediator.Send(command);

        var error = result.GetError();
        Assert.IsTrue(error.Is<ReportNotFoundError>());
    }

    [TestMethod]
    public async Task ShouldReturn_UnauthorizedError_When_User_Is_Unauthorized_To_Delete()
    {
        var testAccount = DataProvider.TestAccountForWitness;
        var invalidAccount = DataProvider.TestAccountForWitness;
        var testReport = DataProvider.GetReport(testAccount.Id);
        await DbContext.Accounts.AddRangeAsync([testAccount, invalidAccount]);
        await DbContext.Reports.AddAsync(testReport);
        await DbContext.SaveChangesAsync();
        GenerateTokenAndInvoke(invalidAccount);

        var command = new DeleteReportCommand(testReport.Id);
        var result = await Mediator.Send(command);

        var error = result.GetError();
        Assert.IsTrue(error.Is<UnauthorizedError>());
    }
}
