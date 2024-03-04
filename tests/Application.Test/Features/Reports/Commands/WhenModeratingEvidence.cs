using Application.Features.Reports.Commands.ModerateEvidence;

namespace Application.Test.Features.Reports.Commands;
[TestClass]
public class WhenModeratingEvidence : TestBase
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
    public async Task ShouldReturnTrue_When_EvidenceIs_Pending()
    {
        var testWitness = DataProvider.TestAccountForWitness;
        var testReport = DataProvider.GetReportWithAEvidence(testWitness.Id);
        var evidenceToModerate = testReport.Evidences.First().Id;
        var testModerator = DataProvider.TestAccountForModerator;
        await DbContext.Accounts.AddRangeAsync([testModerator, testWitness]);
        await DbContext.Reports.AddAsync(testReport);
        await SaveAndClearChangeTrackerAsync();
        GenerateTokenAndInvoke(testModerator);

        var command = new ModerateEvidenceCommand(testReport.Id, evidenceToModerate, testModerator.Id);
        var result = await Mediator.Send(command);

        Assert.IsTrue(result.GetValue());
    }

}
