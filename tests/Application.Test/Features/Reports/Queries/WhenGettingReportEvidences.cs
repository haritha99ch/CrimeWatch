using Application.Features.Reports.Queries.GetReportEvidencesById;

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

        var query = new GetReportEvidencesByIdQuery(testReport.Id, null);

        var result = await Mediator.Send(query);
    }
}
