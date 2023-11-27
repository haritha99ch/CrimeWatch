using System.Collections.ObjectModel;
using Domain.AggregateModels.AccountAggregate;
using Persistence.Test.Repositories.AccountRepository.TestSpecifications;

namespace Persistence.Test.Repositories.AccountRepository;

[TestClass]
public class WhenReading : TestBase
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
    public async Task ShouldReturnAccount_PredicateBy_Id()
    {
        var testAccount = DataProvider.TestAccountForModerator;
        await DbContext.Accounts.AddAsync(testAccount);
        await SaveAndClearChangeTrackerAsync();

        var account = await AccountRepository.GetByIdAsync(testAccount.Id);

        Assert.IsNotNull(account);
        Assert.AreEqual(testAccount.Id, account.Id);
    }

    [TestMethod]
    public async Task ShouldAllAccounts()
    {
        var testAccounts = new List<Account>();
        for (int i = 0; i < 5; i++)
        {
            testAccounts.Add(DataProvider.TestAccountForModerator);
        }
        await DbContext.Accounts.AddRangeAsync(testAccounts);
        await SaveAndClearChangeTrackerAsync();

        var accounts = await AccountRepository.GetAllAsync();

        CollectionAssert.AreEquivalent(testAccounts.Select(e => e.Id).ToList(), accounts.Select(e => e.Id).ToList());
    }

    [TestMethod]
    public async Task ShouldCount_NumberOfAccounts()
    {
        var testAccounts = new List<Account>();
        for (int i = 0; i < 5; i++)
        {
            testAccounts.Add(DataProvider.TestAccountForModerator);
        }
        await DbContext.Accounts.AddRangeAsync(testAccounts);
        await SaveAndClearChangeTrackerAsync();

        var accounts = await AccountRepository.CountAsync();
    }

    [TestMethod]
    public async Task ShouldReturnTrue_Exists(){
        var testAccount = DataProvider.TestAccountForModerator;
        await DbContext.Accounts.AddAsync(testAccount);
        await SaveAndClearChangeTrackerAsync();

        var isExists = await AccountRepository.ExistByIdAsync(testAccount.Id);

        Assert.IsTrue(isExists);
    }

    [TestMethod]
    public async Task ShouldReturnAccount_BySpecification(){
        var testAccount = DataProvider.TestAccountForModerator;
        await DbContext.Accounts.AddAsync(testAccount);
        await SaveAndClearChangeTrackerAsync();

        var account = await AccountRepository.GetOneAsync(new AccountByTestSpecification(testAccount.Email));

        Assert.IsNotNull(account);
        Assert.AreEqual(testAccount.Email, account.Email);
    }
}
