using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Test.Accounts;

[TestClass]
public class WhenDeleting : TestBase
{
    [TestInitialize]
    public async Task Initialize()
    {
        await DbContext.Database.EnsureDeletedAsync();
        await DbContext.Database.EnsureCreatedAsync();
    }

    [TestCleanup]
    public async Task Cleanup()
    {
        await DbContext.Database.EnsureDeletedAsync();
    }

    [TestMethod]
    public async Task ShouldDelete()
    {
        var account = DataProvider.TestAccountForWitness;
        await DbContext.Accounts.AddAsync(account);
        await DbContext.SaveChangesAsync();
        DbContext.ChangeTracker.Clear();

        await DbContext.Accounts.Where(a => a.Id == account.Id).ExecuteDeleteAsync();
        await DbContext.SaveChangesAsync();

        var accounts = await DbContext.Accounts.ToListAsync();
        Assert.AreEqual(0, accounts.Count);
    }
}
