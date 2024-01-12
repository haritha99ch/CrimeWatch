using Application.Errors.Common;
using Application.Features.Accounts.Commands.UpdateWitnessAccount;

namespace Application.Test.Features.Accounts.Commands;
[TestClass]
public class WhenUpdatingWitnessAccount : TestBase
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
    public async Task ShouldUpdate_And_Return_Account_When_Authorized()
    {
        var testAccount = DataProvider.TestAccountForWitness;
        await DbContext.Accounts.AddAsync(testAccount);
        await SaveAndClearChangeTrackerAsync();
        GenerateTokenAndInvoke(testAccount);
        var nic = DataProvider.Nic;
        var firstName = DataProvider.FirstName;
        var lastName = DataProvider.LastName;

        var command = new UpdateWitnessAccountCommand(
            testAccount.Id,
            nic,
            firstName,
            lastName,
            testAccount.Person!.Gender,
            testAccount.Person!.BirthDate);
        var result = await Mediator.Send(command);
        var account = result.GetValue();

        Assert.IsNotNull(account);
        Assert.AreEqual(nic, account.Person!.Nic);
        Assert.AreEqual(firstName, account.Person!.FirstName);
        Assert.AreEqual(lastName, account.Person!.LastName);
    }

    [TestMethod]
    public async Task ShouldReturn_UnauthorizedError_When_Authorization_Is_Invalid()
    {
        var testAccount = DataProvider.TestAccountForModerator;
        var invalidAccount = DataProvider.TestAccountForModerator;
        await DbContext.Accounts.AddRangeAsync([testAccount, invalidAccount]);
        await SaveAndClearChangeTrackerAsync();
        GenerateTokenAndInvoke(testAccount);
        var nic = DataProvider.Nic;
        var firstName = DataProvider.FirstName;
        var lastName = DataProvider.LastName;

        var command = new UpdateWitnessAccountCommand(
            testAccount.Id,
            nic,
            firstName,
            lastName,
            testAccount.Person!.Gender,
            testAccount.Person!.BirthDate);
        var result = await Mediator.Send(command);
        var error = result.GetError();

        Assert.IsTrue(error.Is<UnauthorizedError>());
    }
}
