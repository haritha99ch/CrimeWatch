using Application.Features.Reports.Commands.UpdateReport;
using Domain.AggregateModels.ReportAggregate.ValueObjects;

namespace Application.Test.Features.Reports.Commands;
[TestClass]
public class WhenUpdatingReport : TestBase
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
    public async Task Should_Update_Report()
    {
        var testAccount = DataProvider.TestAccountForModerator;
        var testReport = DataProvider.GetReport(testAccount.Id);
        await DbContext.Accounts.AddAsync(testAccount);
        await DbContext.Reports.AddAsync(testReport);
        await SaveAndClearChangeTrackerAsync();
        var caption = DataProvider.Caption;
        var description = DataProvider.Description;
        GenerateTokenAndInvoke(testAccount);

        var command = new UpdateReportCommand(
            testReport.Id,
            caption,
            description,
            testReport.Location.No,
            testReport.Location.Street1,
            testReport.Location.Street2,
            testReport.Location.City,
            testReport.Location.Province,
            testReport.ViolationTypes,
            testReport.MediaItem);
        var result = await Mediator.Send(command);

        var report = result.GetValue();
        Assert.AreEqual(report.Caption, caption);
        Assert.AreEqual(report.Description, description);
    }

    [TestMethod]
    public async Task Should_Return_ReportNotFoundError_When_Report_Does_Not_Exists()
    {
        var testAccount = DataProvider.TestAccountForModerator;
        await DbContext.Accounts.AddAsync(testAccount);
        await SaveAndClearChangeTrackerAsync();
        var testReportId = new ReportId(Guid.NewGuid());
        var caption = DataProvider.Caption;
        var description = DataProvider.Description;
        var no = DataProvider.No;
        var street1 = DataProvider.Street1;
        var street2 = DataProvider.Street2;
        var city = DataProvider.City;
        var province = DataProvider.Province;
        var violationTypes = DataProvider.ViolationTypes;
        var mediaItem = DataProvider.TestMediaItem;
        GenerateTokenAndInvoke(testAccount);

        var command = new UpdateReportCommand(
            testReportId,
            caption,
            description,
            no,
            street1,
            street2,
            city,
            province,
            violationTypes,
            null,
            mediaItem);
        var result = await Mediator.Send(command);

        var error = result.GetError();
        Assert.IsTrue(error.Is<ReportNotFoundError>());
    }
}
