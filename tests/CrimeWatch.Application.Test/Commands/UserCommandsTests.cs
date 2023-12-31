﻿using CrimeWatch.Application.Commands.ModeratorCommands.CreateModerator;
using CrimeWatch.Application.Commands.ModeratorCommands.UpdateModerator;
using CrimeWatch.Application.Commands.WitnessCommands.CreateWitness;
using CrimeWatch.Application.Commands.WitnessCommands.UpdateWitness;
using CrimeWatch.Domain.Enums;

namespace CrimeWatch.Application.Test.Commands;
[TestClass]
public class UserCommandsTests : CQRSTests
{
    public UserCommandsTests() : base("UserCommandsTests") { }

    [TestCleanup]
    public async Task TestCleanupAsync()
    {
        // Clean up the database after each test
        _dbContext.Witness.RemoveRange(_dbContext.Witness);
        _dbContext.Moderator.RemoveRange(_dbContext.Moderator);
        await _dbContext.SaveChangesAsync();
    }

    [TestMethod]
    public async Task CreateWitnessCommand_Should_Create_Witness()
    {
        // Arrange
        CreateWitnessCommand command = new(
            FirstName: "John",
            LastName: "Doe",
            Gender.Male,
            new(1990, 1, 1),
            PhoneNumber: "1234567890",
            Email: "john@example.com",
            Password: "P@ssw0rd"
        );

        // Act
        var witness = await _mediator.Send(command);

        // Assert
        Assert.AreEqual(command.FirstName, witness.User!.FirstName);
        Assert.AreEqual(command.LastName, witness.User!.LastName);
        Assert.AreEqual(command.Gender, witness.User!.Gender);
        Assert.AreEqual(command.DateOfBirth, witness.User!.DateOfBirth);
        Assert.AreEqual(command.PhoneNumber, witness.User!.PhoneNumber);
        Assert.AreEqual(command.Email, witness.Account!.Email);
        Assert.AreEqual(command.Password, witness.Account!.Password);
    }

    [TestMethod]
    public async Task CreateModeratorCommand_Should_Create_Moderator()
    {
        // Arrange
        CreateModeratorCommand command = new(
            FirstName: "Jane",
            LastName: "Smith",
            Gender.Female,
            new(1985, 5, 10),
            PhoneNumber: "9876543210",
            PoliceId: "12345",
            Province: "Example Province",
            Email: "jane@example.com",
            Password: "P@ssw0rd"
        );

        // Act
        var moderator = await _mediator.Send(command);

        // Assert
        Assert.AreEqual(command.FirstName, moderator.User!.FirstName);
        Assert.AreEqual(command.LastName, moderator.User!.LastName);
        Assert.AreEqual(command.Gender, moderator.User!.Gender);
        Assert.AreEqual(command.DateOfBirth, moderator.User!.DateOfBirth);
        Assert.AreEqual(command.PhoneNumber, moderator.User!.PhoneNumber);
        Assert.AreEqual(command.Email, moderator.Account!.Email);
        Assert.AreEqual(command.Password, moderator.Account!.Password);
        Assert.AreEqual(command.PoliceId, moderator.PoliceId);
        Assert.AreEqual(command.Province, moderator.Province);
    }

    [TestMethod]
    public async Task UpdateModeratorCommand_Should_Update_Moderator()
    {
        // Arrange
        var moderatorEntry = await _dbContext.Moderator.AddAsync(DataProvider.GetTestModerators().FirstOrDefault()!);
        await _dbContext.SaveChangesAsync();
        _dbContext.ChangeTracker.Clear();

        // Act
        UpdateModeratorCommand command = new(
            moderatorEntry.Entity.Id,
            FirstName: "UpdatedFirstName",
            LastName: "UpdatedLastName",
            Gender.Female,
            new(1990, 1, 1),
            PhoneNumber: "5555555555",
            PoliceId: "54321",
            Province: "Updated Province",
            Email: "updated@example.com",
            Password: "UpdatedP@ssw0rd"
        );
        var updatedModerator = await _mediator.Send(command);

        // Assert
        Assert.AreEqual(command.FirstName, updatedModerator.User!.FirstName);
        Assert.AreEqual(command.LastName, updatedModerator.User!.LastName);
        Assert.AreEqual(command.Gender, updatedModerator.User!.Gender);
        Assert.AreEqual(command.DateOfBirth, updatedModerator.User!.DateOfBirth);
        Assert.AreEqual(command.PhoneNumber, updatedModerator.User!.PhoneNumber);
        Assert.AreEqual(command.Email, updatedModerator.Account!.Email);
        Assert.AreEqual(command.Password, updatedModerator.Account!.Password);
        Assert.AreEqual(command.PoliceId, updatedModerator.PoliceId);
        Assert.AreEqual(command.Province, updatedModerator.Province);
    }

    [TestMethod]
    public async Task UpdateWitnessCommand_Should_Update_Witness()
    {
        // Arrange
        var witnessEntry = await _dbContext.Witness.AddAsync(DataProvider.GetTestWitness().FirstOrDefault()!);
        await _dbContext.SaveChangesAsync();
        _dbContext.ChangeTracker.Clear();

        // Act
        UpdateWitnessCommand command = new(
            witnessEntry.Entity.Id,
            FirstName: "UpdatedFirstName",
            LastName: "UpdatedLastName",
            Gender.Male,
            new(1988, 6, 15),
            PhoneNumber: "6666666666",
            Email: "updated@example.com",
            Password: "UpdatedP@ssw0rd"
        );
        var updatedWitness = await _mediator.Send(command);

        // Assert
        Assert.AreEqual(command.FirstName, updatedWitness.User!.FirstName);
        Assert.AreEqual(command.LastName, updatedWitness.User!.LastName);
        Assert.AreEqual(command.Gender, updatedWitness.User!.Gender);
        Assert.AreEqual(command.DateOfBirth, updatedWitness.User!.DateOfBirth);
        Assert.AreEqual(command.PhoneNumber, updatedWitness.User!.PhoneNumber);
        Assert.AreEqual(command.Email, updatedWitness.Account!.Email);
        Assert.AreEqual(command.Password, updatedWitness.Account!.Password);
    }
}
