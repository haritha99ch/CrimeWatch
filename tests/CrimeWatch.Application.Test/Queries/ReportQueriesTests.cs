using CrimeWatch.Application.Queries.ReportQueries.GetAllModeratedReports;
using CrimeWatch.Application.Queries.ReportQueries.GetAllReports;
using CrimeWatch.Application.Queries.ReportQueries.GetModeratorReports;
using CrimeWatch.Application.Queries.ReportQueries.GetWitnessReports;

namespace CrimeWatch.Application.Test.Queries;
[TestClass]
public class ReportQueriesTests : CQRSTests
{
    public ReportQueriesTests() : base("Report") { }

    [TestInitialize]
    public async Task TestInitializeAsync()
    {
        // Add test data to the in-memory database
        await _dbContext.Witness.AddRangeAsync(DataProvider.GetTestWitness());
        await _dbContext.Moderator.AddRangeAsync(DataProvider.GetTestModerators());
        await _dbContext.SaveChangesAsync();
    }

    [TestCleanup]
    public async Task TestCleanupAsync()
    {
        // Clean up the database after each test
        _dbContext.Report.RemoveRange(_dbContext.Report);
        _dbContext.Witness.RemoveRange(_dbContext.Witness);
        _dbContext.Moderator.RemoveRange(_dbContext.Moderator);
        await _dbContext.SaveChangesAsync();
    }

    [TestMethod]
    public async Task GetAllReportsQuery_Should_Return_AllReport()
    {
        // Arrange
        var reports = DataProvider.GetTestReports();
        reports.ForEach(e => e.Witness = DataProvider.GetTestWitness().FirstOrDefault());
        await _dbContext.Report.AddRangeAsync(reports);
        await _dbContext.SaveChangesAsync();

        // Act
        var getReports = await _mediator.Send(new GetAllReportsQuery());

        // Assert
        Assert.AreEqual(reports.Count, getReports.Count);
    }

    [TestMethod]
    public async Task GetAllModeratedReportsQuery_Should_Return_AllModeratedReports()
    {
        // Arrange
        var reports = DataProvider.GetTestReports();
        reports.ForEach(e => e.Witness = DataProvider.GetTestWitness().FirstOrDefault());
        reports.ForEach(e => e.Approve());
        reports.AddRange(DataProvider.GetTestReports());
        await _dbContext.Report.AddRangeAsync(reports);
        await _dbContext.SaveChangesAsync();

        var resultReports = await _mediator.Send(new GetAllModeratedReportsQuery());

        // Assert
        Assert.AreNotEqual(reports.Count, resultReports.Count);
    }

    [TestMethod]
    public async Task GetWitnessReportQuery_Should_Return_WitnessReports()
    {
        // Arrange
        var witness = await _dbContext.Witness.FirstOrDefaultAsync();
        var report = DataProvider.GetTestReports().FirstOrDefault()!;
        report.WitnessId = witness!.Id;

        await _dbContext.Report.AddAsync(report);
        await _dbContext.SaveChangesAsync();

        // Act
        var resultReports = await _mediator.Send(new GetWitnessReportsQuery(witness.Id));

        // Assert
        Assert.AreEqual(1, resultReports.Count);
    }

    [TestMethod]
    public async Task GetModeratorReportQuery_Should_Return_WitnessReports()
    {
        // Arrange
        var witness = await _dbContext.Witness.FirstOrDefaultAsync();
        var moderator = await _dbContext.Moderator.FirstOrDefaultAsync();
        var report = DataProvider.GetTestReports().FirstOrDefault()!;
        report.ModeratorId = moderator!.Id;
        report.WitnessId = witness!.Id;

        await _dbContext.Report.AddAsync(report);
        await _dbContext.SaveChangesAsync();

        // Act
        var resultReports = await _mediator.Send(new GetModeratorReportsQuery(moderator.Id));

        // Assert
        Assert.AreEqual(1, resultReports.Count);
    }
}
