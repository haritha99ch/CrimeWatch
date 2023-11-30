using Application.Errors.Accounts;
using Application.Features.Accounts.Queries.SignInToAccount;

namespace Application.Test.Features.Accounts.Queries;

[TestClass]
public class WhenSignInToAccount : TestBase
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
    public async Task ShouldReturn_Token()
    {
        var password = DataProvider.Password;
        var account = DataProvider.GetModeratorAccount(password);
        await DbContext.Accounts.AddAsync(account);
        await SaveAndClearChangeTrackerAsync();

        var command = new SignInToAccountQuery(account.Email, password);
        var result = await Mediator.Send(command);
        var token = result.GetValue();

        Assert.IsNotNull(token);
    }

    [TestMethod]
    public async Task When_Email_Is_Incorrect_Should_Return_AccountNotFoundError()
    {
        var password = DataProvider.Password;
        var account = DataProvider.GetModeratorAccount(password);
        await DbContext.Accounts.AddAsync(account);
        await SaveAndClearChangeTrackerAsync();
        var incorrectEmail = DataProvider.Email;

        var command = new SignInToAccountQuery(incorrectEmail, password);
        var result = await Mediator.Send(command);
        var error = result.GetError();

        Assert.IsTrue(error.Is<AccountNotFoundError>());
    }
}
