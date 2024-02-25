using Application.Features.Reports.Queries.GetReports;
using Domain.AggregateModels.ReportAggregate.Enums;

namespace Application.Test.Features.Reports.Queries;
[TestClass]
public class WhenGettingReports : TestBase
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
    public async Task ShouldReturn_AllCurrentUser_Reports()
    {
        var testCurrentWitness = DataProvider.TestAccountForWitness;
        var testWitness = DataProvider.TestAccountForWitness;
        var testReports = Enumerable.Range(1, 10)
            .Select(e => e <= Random.Shared.Next(3, 7)
                ? DataProvider.GetReport(testCurrentWitness.Id)
                : DataProvider.GetReport(testWitness.Id))
            .ToList();
        var expectedReportCount = testReports.Count(e => e.AuthorId!.Equals(testCurrentWitness.Id));
        await DbContext.Accounts.AddRangeAsync([testWitness, testCurrentWitness]);
        await DbContext.Reports.AddRangeAsync(testReports);
        await DbContext.SaveChangesAsync();
        GenerateTokenAndInvoke(testCurrentWitness);

        var query = new GetReportsQuery();
        var result = await Mediator.Send(query);

        var reports = result.GetValue();
        Assert.AreEqual(expectedReportCount, reports.Count);
        reports.ForEach(report => Assert.AreEqual(testCurrentWitness.Id, report.AuthorDetails!.AccountId));
    }

    [TestMethod]
    public async Task ShouldReturn_AllCurrentUser_Reports_With_ApprovedReports()
    {
        var testCurrentWitness = DataProvider.TestAccountForWitness;
        var testWitness = DataProvider.TestAccountForWitness;
        var testReports = Enumerable.Range(1, 10)
            .Select(e => e <= Random.Shared.Next(3, 7)
                ? DataProvider.GetReport(testCurrentWitness.Id)
                : DataProvider.GetReport(testWitness.Id))
            .ToList();
        testReports.ForEach(e =>
            {
                if (Random.Shared.Next(2) == 0)
                    e.SetApproved();
            }
        );
        await DbContext.Accounts.AddRangeAsync([testWitness, testCurrentWitness]);
        await DbContext.Reports.AddRangeAsync(testReports);
        await DbContext.SaveChangesAsync();
        GenerateTokenAndInvoke(testCurrentWitness);
        var expectedReportCount = testReports.Count(e =>
            e.Status.Equals(Status.Approved)
            || e.AuthorId!.Equals(testCurrentWitness.Id));

        var query = new GetReportsQuery();
        var result = await Mediator.Send(query);

        var reports = result.GetValue();
        Assert.AreEqual(expectedReportCount, reports.Count);
        reports.ForEach(e =>
            Assert.IsTrue(e.Status.Equals(Status.Approved)
                || e.AuthorDetails!.AccountId!.Equals(testCurrentWitness.Id)));
    }
}
