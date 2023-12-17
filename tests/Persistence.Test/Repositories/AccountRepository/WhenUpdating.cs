namespace Persistence.Test.Repositories.AccountRepository;
[TestClass]
public class WhenUpdating : TestBase
{
    [TestInitialize]
    public async Task Initialize()
    {
        await base.InitializeAsync();
    }

    [TestCleanup]
    public async Task Cleanup()
    {
        await base.CleanupAsync();
    }

    [TestMethod]
    public async Task ShouldUpdate()
    {
        var testAccount = DataProvider.TestAccountForModerator;
        await DbContext.Accounts.AddAsync(testAccount);
        await DbContext.SaveChangesAsync();

        var newFirstName = DataProvider.FirstName;
        var newLastName = DataProvider.LastName;
        var newPoliceId = DataProvider.PoliceId;
        testAccount.UpdateModerator(
            testAccount.Person!.Nic,
            newFirstName,
            newLastName,
            testAccount.Person.Gender,
            testAccount.Person.BirthDate,
            newPoliceId,
            testAccount.Moderator!.City,
            testAccount.Moderator.Province);
        var updatedAccount = await AccountRepository.UpdateAsync(testAccount);

        Assert.AreEqual(newFirstName, updatedAccount.Person!.FirstName);
        Assert.AreEqual(newLastName, updatedAccount.Person!.LastName);
        Assert.AreEqual(newPoliceId, updatedAccount.Moderator!.PoliceId);
    }
}
