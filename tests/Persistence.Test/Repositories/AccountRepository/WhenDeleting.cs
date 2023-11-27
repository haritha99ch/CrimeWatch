using Microsoft.EntityFrameworkCore;

namespace Persistence.Test.Repositories.AccountRepository;

[TestClass]
public class WhenDeleting : TestBase
{
    [TestInitialize]
    public async Task Initialize(){
        await base.InitializeAsync();
    }

    [TestCleanup]
    public async Task Cleanup(){
        await base.CleanupAsync();
    }

    [TestMethod]
    public async Task ShouldDeleteAccount(){
        var testAccount = DataProvider.TestAccountForModerator;
        await DbContext.Accounts.AddAsync(testAccount);
        await DbContext.SaveChangesAsync();

        var deleted = await AccountRepository.DeleteByIdAsync(testAccount.Id);
        var account = await DbContext.Accounts.FirstOrDefaultAsync(a=>a.Id.Equals(testAccount.Id));

        Assert.IsTrue(deleted);
        Assert.IsNull(account);
    }
}
