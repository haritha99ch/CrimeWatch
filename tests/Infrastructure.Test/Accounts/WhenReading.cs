using Domain.AggregateModels.AccountAggregate;
using Domain.AggregateModels.AccountAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Test.Accounts;
[TestClass]
public class WhenReading : TestBase
{
    private Account Account { get; set; } = default!;

    [TestInitialize]
    public async Task Initialize()
    {
        await DbContext.Database.EnsureDeletedAsync();
        await DbContext.Database.EnsureCreatedAsync();

        var account = DataProvider.TestAccountForModerator;
        Account = account;
        DbContext.Accounts.Add(account);
        await DbContext.SaveChangesAsync();

        DbContext.ChangeTracker.Clear();
    }

    [TestCleanup]
    public async Task Cleanup()
    {
        await DbContext.Database.EnsureDeletedAsync();
    }

    [TestMethod]
    public async Task When_ReadingById()
    {
        var account = await DbContext.Accounts.FindAsync(Account.Id);

        Assert.IsNotNull(account);
    }

    [TestMethod]
    public async Task When_ReadingById_IncludingOwnedEntities()
    {
        var account = await DbContext
            .Accounts
            .Include(e => e.Moderator)
            .Include(e => e.Person)
            .FirstOrDefaultAsync(e => e.Id.Equals(Account.Id));

        Assert.IsNotNull(account);
        Assert.IsNotNull(account.Moderator);
        Assert.IsNotNull(account.Person);
    }

    [TestMethod]
    public async Task When_ReadingByPredicate()
    {
        var account = await DbContext
            .Accounts
            .Where(e => e.Email.Equals(Account.Email) && e.Password.Equals(Account.Password))
            .FirstOrDefaultAsync();

        Assert.IsNotNull(account);
    }

    [TestMethod]
    public async Task When_ReadingByPredicating_From_OwnedEntity()
    {
        var account = await DbContext
            .Accounts
            .Where(e => e.Person!.FirstName.Equals(Account.Person!.FirstName))
            .FirstOrDefaultAsync();

        Assert.IsNotNull(account);
    }

    [TestMethod]
    public async Task When_Selecting_Properties()
    {
        var account = await DbContext
            .Accounts
            .Select(e => new { e.Id, e.Email })
            .FirstOrDefaultAsync();

        Assert.IsNotNull(account);
        Assert.IsNotNull(account.Id);
        Assert.IsNotNull(account.Email);
    }

    [TestMethod]
    public async Task When_Selecting_Properties_From_OwnedEntities()
    {
        var account = await DbContext
            .Accounts
            .Select(e => new { e.Id, e.Person!.FirstName })
            .FirstOrDefaultAsync(e => e.Id.Equals(Account.Id));

        Assert.IsNotNull(account);
        Assert.IsNotNull(account.Id);
        Assert.IsNotNull(account.FirstName);
    }

    [TestMethod]
    public async Task When_Selecting_And_Mapping_To_Type()
    {
        var account = await DbContext
            .Accounts
            .Where(e => e.Id.Equals(Account.Id))
            .Select(
                e =>
                    new AccountWithPersonalInfo(
                        e.Id,
                        e.Email,
                        $"{e.Person!.FirstName} {e.Person.LastName}"))
            .SingleOrDefaultAsync();

        Assert.IsNotNull(account);
        Assert.IsNotNull(account.AccountId);
        Assert.IsNotNull(account.Email);
        Assert.IsNotNull(account.FullName);
        Assert.IsTrue(account.GetType() == typeof(AccountWithPersonalInfo));
    }

    private record AccountWithPersonalInfo(AccountId AccountId, string Email, string FullName);
}
