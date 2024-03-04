using Application.Errors.Common;
using Application.Features.Reports.Commands.ApproveEvidence;

namespace Application.Test.Features.Reports.Commands;
[TestClass]
public class WhenApprovingEvidence : TestBase
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
    public async Task ShouldReturnTrue_When_CurrentUser_Is_ReviewingTheEvidence()
    {
        var testWitness = DataProvider.TestAccountForWitness;
        var testModerator = DataProvider.TestAccountForModerator;
        var testReport = DataProvider.GetReportWithAEvidence(testWitness.Id);
        var evidenceToModerate = testReport.Evidences.First();
        testReport.SetModeratorModeratorForEvidence(evidenceToModerate.Id, testModerator.Id);
        await DbContext.Accounts.AddRangeAsync([testModerator, testWitness]);
        await DbContext.Reports.AddAsync(testReport);
        await SaveAndClearChangeTrackerAsync();
        GenerateTokenAndInvoke(testModerator);

        var command = new ApproveEvidenceCommand(testReport.Id, evidenceToModerate.Id);
        var result = await Mediator.Send(command);

        Assert.IsTrue(result.GetValue());
    }
    
    [TestMethod]
    public async Task ShouldReturn_UnauthorizedError_When_CurrentUser_IsAWitness()
    {
        var testWitness = DataProvider.TestAccountForWitness;
        var testModerator = DataProvider.TestAccountForModerator;
        var testReport = DataProvider.GetReportWithAEvidence(testWitness.Id);
        var evidenceToModerate = testReport.Evidences.First();
        testReport.SetModeratorModeratorForEvidence(evidenceToModerate.Id, testModerator.Id);
        await DbContext.Accounts.AddRangeAsync([testModerator, testWitness]);
        await DbContext.Reports.AddAsync(testReport);
        await SaveAndClearChangeTrackerAsync();
        GenerateTokenAndInvoke(testWitness);

        var command = new ApproveEvidenceCommand(testReport.Id, evidenceToModerate.Id);
        var result = await Mediator.Send(command);

        Assert.IsTrue(result.GetError().Is<UnauthorizedError>());
    }
}
