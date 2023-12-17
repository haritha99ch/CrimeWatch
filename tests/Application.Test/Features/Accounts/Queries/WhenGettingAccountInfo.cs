using Application.Errors.Accounts;
using Application.Features.Accounts.Queries.GetAccountInfoById;
using Domain.AggregateModels.AccountAggregate.Enums;

namespace Application.Test.Features.Accounts.Queries;
[TestClass]
public class WhenGettingAccountInfoById : TestBase
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
    public async Task ShouldReturn_AccountInfo()
    {
        var testAccount = DataProvider.TestAccountForModerator;
        await DbContext.Accounts.AddAsync(testAccount);
        await SaveAndClearChangeTrackerAsync();

        var result = await Mediator.Send(new GetAccountInfoByIdQuery(testAccount.Id));
        var accountInfo = result.GetValue();

        Assert.AreEqual(testAccount.Email, accountInfo.Email);
        Assert.AreEqual(
            $"{testAccount.Person!.FirstName} {testAccount.Person!.LastName}",
            accountInfo.FullName);
        Assert.AreEqual(
            testAccount.AccountType.Equals(AccountType.Moderator),
            accountInfo.IsModerator);
    }

    [TestMethod]
    public async Task ShouldReturn_AccountNotFoundError_When_Passing_Invalid_AccountId()
    {
        var testAccountId = DataProvider.AccountId;

        var result = await Mediator.Send(new GetAccountInfoByIdQuery(testAccountId));
        var error = result.GetError();

        Assert.IsTrue(error.Is<AccountNotFoundError>());
    }
}
