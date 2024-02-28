using Application.Errors.Accounts;
using Application.Errors.Common;
using Application.Features.Reports.Commands.RemoveEvidence;
using Microsoft.EntityFrameworkCore;

namespace Application.Test.Features.Reports.Commands;
[TestClass]
public class WhenRemovingEvidence : TestBase
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
    public async Task ShouldRemoveEvidence_When_User_Is_Authorized()
    {
        var testWitness = DataProvider.TestAccountForWitness;
        var testReport = DataProvider.GetReportWithAEvidence(testWitness.Id);
        await DbContext.Accounts.AddAsync(testWitness);
        await DbContext.Reports.AddAsync(testReport);
        await SaveAndClearChangeTrackerAsync();
        GenerateTokenAndInvoke(testWitness);

        var command = new RemoveEvidenceCommand(testReport.Id, testReport.Evidences.First().Id);
        var result = await Mediator.Send(command);

        Assert.IsTrue(result.GetValue());
        var report = await DbContext.Reports.Include(e => e.Evidences).FirstOrDefaultAsync(r => r.Id == testReport.Id);
        Assert.AreEqual(testReport.Evidences.Count - 1, report!.Evidences.Count);
    }

    [TestMethod]
    public async Task ShouldReturn_UnableToAuthenticateError_When_User_Is_Not_Logged_In()
    {
        var testWitness = DataProvider.TestAccountForWitness;
        var testReport = DataProvider.GetReportWithAEvidence(testWitness.Id);
        await DbContext.Accounts.AddAsync(testWitness);
        await DbContext.Reports.AddAsync(testReport);
        await SaveAndClearChangeTrackerAsync();

        var command = new RemoveEvidenceCommand(testReport.Id, testReport.Evidences.First().Id);
        var result = await Mediator.Send(command);

        Assert.IsTrue(result.GetError().Is<UnableToAuthenticateError>());
    }

    [TestMethod]
    public async Task ShouldReturn_UnauthorizedError_When_User_Is_Not_Authorized()
    {
        var testWitness = DataProvider.TestAccountForWitness;
        var currentUser = DataProvider.TestAccountForWitness;
        var testReport = DataProvider.GetReportWithAEvidence(testWitness.Id);
        await DbContext.Accounts.AddRangeAsync([testWitness, currentUser]);
        await DbContext.Reports.AddAsync(testReport);
        await SaveAndClearChangeTrackerAsync();
        GenerateTokenAndInvoke(currentUser);

        var command = new RemoveEvidenceCommand(testReport.Id, testReport.Evidences.First().Id);
        var result = await Mediator.Send(command);

        Assert.IsTrue(result.GetError().Is<UnauthorizedError>());
    }
    
    [TestMethod]
    public async Task ShouldRemoveEvidence_When_User_Is_Moderator()
    {
        var testWitness = DataProvider.TestAccountForWitness;
        var testModerator = DataProvider.TestAccountForModerator;
        var testReport = DataProvider.GetReportWithAEvidence(testWitness.Id);
        await DbContext.Accounts.AddRangeAsync([testWitness, testModerator]);
        await DbContext.Reports.AddAsync(testReport);
        await SaveAndClearChangeTrackerAsync();
        GenerateTokenAndInvoke(testModerator);

        var command = new RemoveEvidenceCommand(testReport.Id, testReport.Evidences.First().Id);
        var result = await Mediator.Send(command);

        Assert.IsTrue(result.GetValue());
        var report = await DbContext.Reports.Include(e => e.Evidences).FirstOrDefaultAsync(r => r.Id == testReport.Id);
        Assert.AreEqual(testReport.Evidences.Count - 1, report!.Evidences.Count);
    }
}
