using CrimeWatch.Application.Commands.ReportCommands.ApproveReport;
using CrimeWatch.Application.Commands.ReportCommands.CreateReport;
using CrimeWatch.Application.Commands.ReportCommands.DeclineReport;
using CrimeWatch.Application.Commands.ReportCommands.ModerateReport;
using CrimeWatch.Application.Commands.ReportCommands.RevertReportToReview;
using CrimeWatch.Application.Commands.ReportCommands.UpdateReport;
using CrimeWatch.Domain.AggregateModels.ReportAggregate;
using CrimeWatch.Domain.Enums;
using CrimeWatch.Domain.ValueObjects;

namespace CrimeWatch.Application.Test.Commands;
[TestClass]
public class ReportCommandsTests : CQRSTests
{
    private ReportId? ReportId { get; set; }

    public ReportCommandsTests() : base("Reports") { }

    [TestInitialize]
    public async Task TestInitializeAsync()
    {
        // Add test data to the in-memory database
        var testWitness = DataProvider.GetTestWitness().FirstOrDefault()!;
        var testModerator = DataProvider.GetTestModerators().FirstOrDefault()!;
        var testReport = DataProvider.GetTestReports().FirstOrDefault()!;
        testReport.WitnessId = testWitness.Id;
        ReportId = testReport.Id;
        await _dbContext.Witness.AddAsync(testWitness);
        await _dbContext.Moderator.AddAsync(testModerator);
        await _dbContext.Report.AddAsync(testReport);
        await _dbContext.SaveChangesAsync();
    }

    [TestCleanup]
    public async Task TestCleanupAsync()
    {
        // Clean up the database after each test
        _dbContext.Witness.RemoveRange(_dbContext.Witness);
        _dbContext.Report.RemoveRange(_dbContext.Report);
        _dbContext.Moderator.RemoveRange(_dbContext.Moderator);
        await _dbContext.SaveChangesAsync();
    }

    [TestMethod]
    public async Task CreateReportCommand_Should_Create_Report()
    {
        // Arrange
        var witness = await _dbContext.Witness.FirstOrDefaultAsync();
        var mediaItem = DataProvider.GetTestFile();

        CreateReportCommand command = new(
            witness!.Id,
            Title: "Sample Report Title",
            Description: "Sample Report Description",
            DataProvider.GetTestLocation(),
            mediaItem
        );

        // Act
        var report = await _mediator.Send(command);

        // Assert
        Assert.IsNotNull(report);
        Assert.AreEqual(command.WitnessId, report.WitnessId);
        Assert.AreEqual(command.Title, report.Title);
        Assert.AreEqual(command.Description, report.Description);
        Assert.AreEqual(command.Location, report.Location);
    }

    [TestMethod]
    public async Task UpdateReportCommand_Should_Update_Report()
    {
        // Arrange
        var newTitle = "Updated Title";
        var newDescription = "Updated Description";
        var newLocation = DataProvider.GetTestLocation();
        var newMediaItem = MediaItem.Create(MediaItemType.Video, url: "url updated");

        UpdateReportCommand command = new(
            ReportId!,
            newTitle,
            newDescription,
            newLocation,
            newMediaItem
        );

        // Act
        var updatedReport = await _mediator.Send(command);

        // Assert
        Assert.IsNotNull(updatedReport);
        Assert.AreEqual(command.Title, updatedReport.Title);
        Assert.AreEqual(command.Description, updatedReport.Description);
        Assert.AreEqual(command.Location, updatedReport.Location);
        Assert.AreEqual(command.MediaItem, updatedReport.MediaItem);
    }

    [TestMethod]
    public async Task ApproveReportCommand_Should_Approve_Report()
    {
        // Arrange
        var report = await _dbContext.Report.FirstOrDefaultAsync();
        ApproveReportCommand command = new(report!.Id);

        // Act
        var approvedReport = await _mediator.Send(command);

        // Assert
        Assert.IsTrue(approvedReport.Status.Equals(Status.Approved));
    }

    [TestMethod]
    public async Task DeclineReportCommand_Should_Decline_Report()
    {
        // Arrange
        var report = await _dbContext.Report.FirstOrDefaultAsync();
        DeclineReportCommand command = new(report!.Id);

        // Act
        var declinedReport = await _mediator.Send(command);

        // Assert
        Assert.IsTrue(declinedReport.Status.Equals(Status.Declined));
    }

    [TestMethod]
    public async Task ModerateReportCommand_Should_Moderate_Report()
    {
        // Arrange
        var report = await _dbContext.Report.FirstOrDefaultAsync();
        var moderator = await _dbContext.Moderator.FirstOrDefaultAsync();
        ModerateReportCommand command = new(report!.Id, moderator!.Id);

        // Act
        var moderatedReport = await _mediator.Send(command);

        // Assert
        Assert.AreEqual(moderator.Id, moderatedReport.ModeratorId);
        Assert.IsTrue(moderatedReport.Status.Equals(Status.UnderReview));
    }

    [TestMethod]
    public async Task RevertReportToReviewCommand_Should_Revert_Report_To_Review()
    {
        // Arrange
        var report = await _dbContext.Report.FirstOrDefaultAsync();
        RevertReportToReviewCommand command = new(report!.Id);

        // Act
        var revertedReport = await _mediator.Send(command);

        // Assert
        Assert.IsTrue(revertedReport.Status.Equals(Status.UnderReview));
    }
}
