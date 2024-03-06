using Application.Features.Reports.Commands.RevertEvidenceToUnderReview;

namespace Application.Test.Features.Reports.Commands;
[TestClass]
public class WhenRevertingEvidenceToUnderReview : TestBase
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
        var evidenceToDecline = testReport.Evidences.First();
        testReport.SetModeratorModeratorForEvidence(evidenceToDecline.Id, testModerator.Id);
        testReport.SetApproveEvidence(evidenceToDecline.Id);
        await DbContext.Accounts.AddRangeAsync([testModerator, testWitness]);
        await DbContext.Reports.AddAsync(testReport);
        await SaveAndClearChangeTrackerAsync();
        GenerateTokenAndInvoke(testModerator);

        var command = new RevertEvidenceToUnderReviewCommand(testReport.Id, evidenceToDecline.Id);
        var result = await Mediator.Send(command);

        Assert.IsTrue(result.GetValue());
    }
}
