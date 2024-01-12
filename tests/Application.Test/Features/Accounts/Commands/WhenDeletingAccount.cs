using Application.Errors.Common;
using Application.Features.Accounts.Commands.DeleteAccount;

namespace Application.Test.Features.Accounts.Commands;
[TestClass]
public class WhenDeletingAccount : TestBase
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
    public async Task Should_Delete_Account_When_Authorized()
    {
        var testAccount = DataProvider.TestAccountForModerator;
        await DbContext.Accounts.AddAsync(testAccount);
        await SaveAndClearChangeTrackerAsync();
        GenerateTokenAndInvoke(testAccount);

        var result = await Mediator.Send(new DeleteAccountCommand(testAccount.Id));
        var deleted = result.GetValue();

        Assert.IsTrue(deleted);
    }

    [TestMethod]
    public async Task Should_Return_UnauthorizedError_When_Authorization_Is_Invalid()
    {
        var testAccount = DataProvider.TestAccountForModerator;
        var invalidAccount = DataProvider.TestAccountForModerator;
        await DbContext.Accounts.AddRangeAsync([testAccount, invalidAccount]);
        await SaveAndClearChangeTrackerAsync();
        GenerateTokenAndInvoke(testAccount);

        var result = await Mediator.Send(new DeleteAccountCommand(testAccount.Id));
        var error = result.GetError();

        Assert.IsTrue(error.Is<UnauthorizedError>());
    }
}
