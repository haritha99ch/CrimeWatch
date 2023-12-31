﻿using CrimeWatch.Application.Commands.EvidenceCommands.ApproveEvidence;
using CrimeWatch.Application.Commands.EvidenceCommands.CreateEvidence;
using CrimeWatch.Application.Commands.EvidenceCommands.DeclineEvidence;
using CrimeWatch.Application.Commands.EvidenceCommands.ModerateEvidence;
using CrimeWatch.Application.Commands.EvidenceCommands.RevertEvidenceToReview;
using CrimeWatch.Application.Commands.EvidenceCommands.UpdateEvidence;
using CrimeWatch.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace CrimeWatch.Application.Test.Commands;
[TestClass]
public class EvidenceCommandsTests : CQRSTests
{
    public EvidenceCommandsTests() : base("Evidence") { }

    [TestInitialize]
    public async Task TestInitializeAsync()
    {
        // Add test data to the in-memory database
        var testWitness = DataProvider.GetTestWitness().FirstOrDefault()!;
        var testModerator = DataProvider.GetTestModerators().FirstOrDefault()!;
        var testReport = DataProvider.GetTestReports().FirstOrDefault()!;
        var testEvidence = DataProvider.GetTestEvidence().FirstOrDefault()!;
        testEvidence.WitnessId = testWitness.Id;
        testEvidence.ReportId = testReport.Id;

        await _dbContext.Witness.AddAsync(testWitness);
        await _dbContext.Moderator.AddAsync(testModerator);
        await _dbContext.Report.AddAsync(testReport);
        await _dbContext.Evidence.AddAsync(testEvidence);
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
    public async Task CreateEvidenceCommand_Should_Create_Evidence()
    {
        // Arrange
        var witness = await _dbContext.Witness.FirstOrDefaultAsync();
        var report = await _dbContext.Report.FirstOrDefaultAsync();
        var location = DataProvider.GetTestLocation();
        List<IFormFile> files = new() { DataProvider.GetTestFile(), DataProvider.GetTestFile() };

        CreateEvidenceCommand command = new(
            WitnessId: witness!.Id,
            ReportId: report!.Id,
            Caption: "Sample Evidence Caption",
            Description: "Sample Evidence Description",
            Location: location,
            MediaItems: files
        );

        // Act
        var evidence = await _mediator.Send(command);

        // Assert
        Assert.IsNotNull(evidence);
        Assert.AreEqual(command.WitnessId, evidence.WitnessId);
        Assert.AreEqual(command.ReportId, evidence.ReportId);
        Assert.AreEqual(command.Caption, evidence.Title);
        Assert.AreEqual(command.Description, evidence.Description);
        Assert.AreEqual(command.Location, evidence.Location);
    }

    [TestMethod]
    public async Task UpdateEvidenceCommand_Should_Update_Evidence()
    {
        // Arrange
        var evidence = await _dbContext.Evidence.FirstOrDefaultAsync();
        var newCaption = "Updated Caption";
        var newDescription = "Updated Description";
        var newLocation = DataProvider.GetTestLocation();
        List<IFormFile> files = new() { DataProvider.GetTestFile(), DataProvider.GetTestFile() };
        UpdateEvidenceCommand command = new(evidence!.Id, newCaption, newDescription, newLocation, string.Empty, files);

        // Act
        var updatedEvidence = await _mediator.Send(command);

        // Assert
        Assert.IsNotNull(updatedEvidence);
    }

    [TestMethod]
    public async Task ModerateEvidenceCommand_Should_Moderate_Evidence()
    {
        // Arrange
        var evidence = await _dbContext.Evidence.FirstOrDefaultAsync();
        var moderator = await _dbContext.Moderator.FirstOrDefaultAsync();

        // Act
        ModerateEvidenceCommand command = new(evidence!.Id, moderator!.Id);
        var result = await _mediator.Send(command);

        // Assert
        Assert.AreEqual(moderator.Id, result.ModeratorId);
        Assert.IsTrue(result.Status.Equals(Status.UnderReview));
    }

    [TestMethod]
    public async Task ApproveEvidenceCommand_Should_Approve_Evidence()
    {
        // Arrange
        var evidence = await _dbContext.Evidence.FirstOrDefaultAsync();

        // Act
        ApproveEvidenceCommand command = new(evidence!.Id);
        var result = await _mediator.Send(command);

        // Assert
        Assert.IsTrue(result.Status.Equals(Status.Approved));
    }

    [TestMethod]
    public async Task DeclineEvidenceCommand_Should_Decline_Evidence()
    {
        // Arrange
        var evidence = await _dbContext.Evidence.FirstOrDefaultAsync();

        // Act
        DeclineEvidenceCommand command = new(evidence!.Id);
        var result = await _mediator.Send(command);

        // Assert
        Assert.IsTrue(result.Status.Equals(Status.Declined));
    }

    [TestMethod]
    public async Task RevertEvidenceToReviewCommand_Should_Revert_Evidence_To_Review()
    {
        // Arrange
        var evidence = await _dbContext.Evidence.FirstOrDefaultAsync();

        // Act
        RevertEvidenceToReviewCommand command = new(evidence!.Id);
        var result = await _mediator.Send(command);

        // Assert
        Assert.IsTrue(result.Status.Equals(Status.UnderReview));
    }
}
