using Application.Errors;
using Application.Features.Accounts.Queries.GetAccountById;
using Application.Features.Accounts.Queries.GetAccountById.Errors;
using Application.Test.Common.Tests;
using Application.Test.Helpers;
using Domain.AggregateModels.AccountAggregate.Enums;

namespace Application.Test.Features.Accounts.Queries;

[TestClass]
/// <summary>
/// To Authorize call <see cref="TestBase.GenerateTokenAndInvoke(bool, Domain.AggregateModels.AccountAggregate.ValueObjects.AccountId, string)."/>
/// </summary>
public class WhenGettingAccountById : TestBase
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
    public async Task ShouldReturnAccount_When_Authorized()
    {
        var testAccount = DataProvider.TestAccountForModerator;
        await DbContext.Accounts.AddAsync(testAccount);
        await SaveAndClearChangeTrackerAsync();
        GenerateTokenAndInvoke(
            testAccount.AccountType.Equals(AccountType.Moderator),
            testAccount.Id,
            testAccount.Email
        );

        var command = new GetAccountByIdQuery(testAccount.Id);
        var result = await Mediator.Send(command);
        var account = result.GetValue();

        Assert.AreEqual(testAccount.Id, account.Id);
    }

    [TestMethod]
    public async Task ShouldReturn_UnableToAuthenticateTokenError_When_Not_Authorized()
    {
        var testAccount = DataProvider.TestAccountForModerator;
        await DbContext.Accounts.AddAsync(testAccount);
        await SaveAndClearChangeTrackerAsync();

        var command = new GetAccountByIdQuery(testAccount.Id);
        var result = await Mediator.Send(command);
        var error = result.GetError();

        Assert.IsTrue(error.Is<UnableToAuthenticateTokenError>());
    }

    [TestMethod]
    public async Task ShouldReturn_UnauthorizedError_When_Authorization_Is_Invalid()
    {
        var validTestAccount = DataProvider.TestAccountForModerator;
        var invalidTestAccount = DataProvider.TestAccountForModerator;
        await DbContext.Accounts.AddRangeAsync([ validTestAccount, invalidTestAccount ]);
        await SaveAndClearChangeTrackerAsync();
        GenerateTokenAndInvoke(
            invalidTestAccount.AccountType.Equals(AccountType.Moderator),
            invalidTestAccount.Id,
            invalidTestAccount.Email
        );

        var command = new GetAccountByIdQuery(validTestAccount.Id);
        var result = await Mediator.Send(command);
        var error = result.GetError();

        Assert.IsTrue(error.Is<UnauthorizedError>());
    }
}
