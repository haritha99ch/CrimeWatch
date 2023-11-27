using Microsoft.EntityFrameworkCore;

namespace Persistence.Test.Repositories.AccountRepository;

[TestClass]
public class WhenAddingAccount : TestBase
{
    [TestInitialize]
    public async Task Initialize()
    {
        await base.InitializeAsync();
    }

    [TestCleanup]
    public async Task CleanUp()
    {
        await base.CleanupAsync();
    }

    [TestMethod]
    public async Task ShouldAdd_AccountForModerator()
    {
        var accountForModerator = DataProvider.TestAccountForModerator;

        var newAccount = await AccountRepository.AddAsync(accountForModerator);

        var account = await DbContext.Accounts.FirstOrDefaultAsync(a => a.Id.Equals(newAccount.Id));
        Assert.IsNotNull(account);
        Assert.AreEqual(accountForModerator.Id, account.Id);
    }

    [TestMethod]
    public async Task ShouldAdd_AccountForWitness()
    {
        var accountForWitness = DataProvider.TestAccountForWitness;

        var newAccount = await AccountRepository.AddAsync(accountForWitness);

        var account = await DbContext.Accounts.FirstOrDefaultAsync(a => a.Id.Equals(newAccount.Id));
        Assert.IsNotNull(account);
        Assert.AreEqual(accountForWitness.Id, account.Id);
    }
}
