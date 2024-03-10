using Application.Errors.Accounts;
using Application.Errors.Common;
using Application.Features.Reports.Commands.AddEvidence;

namespace Application.Test.Features.Reports.Commands;
[TestClass]
public class WhenAddingEvidence : TestBase
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
    public async Task ShouldAddEvidence_When_Report_Is_Approved()
    {
        var testWitness = DataProvider.TestAccountForWitness;
        var evidenceAdder = DataProvider.TestAccountForWitness;
        var testReport = DataProvider.GetReportWithAEvidence(testWitness.Id);
        var mediaItemsForEvidence = Enumerable.Range(1, 3).Select(_ => DataProvider.File);
        testReport.SetApproved();
        await DbContext.Accounts.AddRangeAsync([testWitness, evidenceAdder]);
        await DbContext.Reports.AddAsync(testReport);
        await SaveAndClearChangeTrackerAsync();
        GenerateTokenAndInvoke(evidenceAdder);

        var command = new AddEvidenceCommand(
            testReport.Id,
            evidenceAdder.Id,
            DataProvider.Caption,
            DataProvider.Description,
            DataProvider.No,
            DataProvider.Street1,
            DataProvider.Street2,
            DataProvider.City,
            DataProvider.Province,
            mediaItemsForEvidence
        );
        var result = await Mediator.Send(command);

        Assert.IsNotNull(result.GetValue());
    }

    [TestMethod]
    public async Task ShouldReturn_UnauthorizedError_When_Report_Is_Not_Approved()
    {
        var testWitness = DataProvider.TestAccountForWitness;
        var evidenceAdder = DataProvider.TestAccountForWitness;
        var testReport = DataProvider.GetReportWithAEvidence(testWitness.Id);
        var mediaItemsForEvidence = Enumerable.Range(1, 3).Select(_ => DataProvider.File);
        await DbContext.Accounts.AddRangeAsync([testWitness, evidenceAdder]);
        await DbContext.Reports.AddAsync(testReport);
        await SaveAndClearChangeTrackerAsync();
        GenerateTokenAndInvoke(evidenceAdder);

        var command = new AddEvidenceCommand(
            testReport.Id,
            evidenceAdder.Id,
            DataProvider.Caption,
            DataProvider.Description,
            DataProvider.No,
            DataProvider.Street1,
            DataProvider.Street2,
            DataProvider.City,
            DataProvider.Province,
            mediaItemsForEvidence
        );
        var result = await Mediator.Send(command);

        Assert.IsTrue(result.GetError().Is<UnauthorizedError>());
    }

    [TestMethod]
    public async Task ShouldReturn_UnableToAuthenticate_When_User_Is_Log_Out()
    {
        var testWitness = DataProvider.TestAccountForWitness;
        var evidenceAdder = DataProvider.TestAccountForWitness;
        var testReport = DataProvider.GetReportWithAEvidence(testWitness.Id);
        var mediaItemsForEvidence = Enumerable.Range(1, 3).Select(_ => DataProvider.File);
        await DbContext.Accounts.AddRangeAsync([testWitness, evidenceAdder]);
        await DbContext.Reports.AddAsync(testReport);
        await SaveAndClearChangeTrackerAsync();

        var command = new AddEvidenceCommand(
            testReport.Id,
            evidenceAdder.Id,
            DataProvider.Caption,
            DataProvider.Description,
            DataProvider.No,
            DataProvider.Street1,
            DataProvider.Street2,
            DataProvider.City,
            DataProvider.Province,
            mediaItemsForEvidence
        );
        var result = await Mediator.Send(command);

        Assert.IsTrue(result.GetError().Is<UnableToAuthenticateError>());
    }
}
