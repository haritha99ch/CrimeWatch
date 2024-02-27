using Application.Features.Reports.Queries.GetReportEvidencesByReportId;
using Domain.Common.Models;

namespace Application.Test.Features.Reports.Queries;
[TestClass]
public class WhenGettingReportEvidences : TestBase
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
    public async Task ShouldReturn_EvidencesList()
    {
        var testWitness = DataProvider.TestAccountForWitness;
        var testReport = DataProvider.GetReportWithAEvidence(testWitness.Id);
        testReport.SetApproved();
        await DbContext.Accounts.AddAsync(testWitness);
        await DbContext.Reports.AddAsync(testReport);
        await DbContext.SaveChangesAsync();
        GenerateTokenAndInvoke(testWitness);

        var query = new GetReportEvidencesByReportIdQuery(testReport.Id);
        var result = await Mediator.Send(query);

        Assert.AreEqual(testReport.Evidences.Count, result.GetValue().Count);
    }

    [TestMethod]
    public async Task ShouldReturn_Only_The_Evidence_By_Current_User()
    {
        var testCurrentWitness = DataProvider.TestAccountForWitness;
        var testWitness = DataProvider.TestAccountForWitness;
        var testReport = DataProvider.GetReportWithAEvidence(testCurrentWitness.Id);
        var evidence = DataProvider.GetEvidence(testWitness.Id);
        testReport.AddEvidence(evidence.AuthorId!,
            evidence.Caption,
            evidence.Description,
            evidence.Location.No,
            evidence.Location.Street1,
            evidence.Location.Street2,
            evidence.Location.City,
            evidence.Location.Province,
            evidence.MediaItems.ConvertAll(e => MediaUpload.Create(e.Url, e.MediaType)));
        testReport.SetApproved();
        await DbContext.Accounts.AddRangeAsync([testWitness, testCurrentWitness]);
        await DbContext.Reports.AddAsync(testReport);
        await DbContext.SaveChangesAsync();
        GenerateTokenAndInvoke(testCurrentWitness);

        var query = new GetReportEvidencesByReportIdQuery(testReport.Id);

        var result = await Mediator.Send(query);
        Assert.AreEqual(testReport.Evidences.Count(e => e.AuthorId!.Equals(testCurrentWitness.Id)),
            result.GetValue().Count);
    }

    [TestMethod]
    public async Task ShouldReturn_All_Evidences_When_CurrentUser_Is_A_Moderator()
    {
        var testCurrentWitness = DataProvider.TestAccountForWitness;
        var testWitness = DataProvider.TestAccountForWitness;
        var testModerator = DataProvider.TestAccountForModerator;
        var testReport = DataProvider.GetReportWithAEvidence(testCurrentWitness.Id);
        var evidence = DataProvider.GetEvidence(testWitness.Id);
        testReport.AddEvidence(evidence.AuthorId!,
            evidence.Caption,
            evidence.Description,
            evidence.Location.No,
            evidence.Location.Street1,
            evidence.Location.Street2,
            evidence.Location.City,
            evidence.Location.Province,
            evidence.MediaItems.ConvertAll(e => MediaUpload.Create(e.Url, e.MediaType)));
        testReport.SetApproved();
        await DbContext.Accounts.AddRangeAsync([testWitness, testCurrentWitness, testModerator]);
        await DbContext.Reports.AddAsync(testReport);
        await DbContext.SaveChangesAsync();
        GenerateTokenAndInvoke(testModerator);

        var query = new GetReportEvidencesByReportIdQuery(testReport.Id);

        var result = await Mediator.Send(query);
        Assert.AreEqual(testReport.Evidences.Count, result.GetValue().Count);
    }
}
