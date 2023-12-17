using Domain.AggregateModels.AccountAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Test.Accounts;
[TestClass]
public class WhenUpdating : TestBase
{
    private AccountId ModeratorAccountId { get; set; } = default!;
    private AccountId WitnessAccountId { get; set; } = default!;

    [TestInitialize]
    public async Task Initialize()
    {
        await DbContext.Database.EnsureDeletedAsync();
        await DbContext.Database.EnsureCreatedAsync();

        var moderatorAccount = DataProvider.TestAccountForModerator;
        ModeratorAccountId = moderatorAccount.Id;
        DbContext.Accounts.Add(moderatorAccount);
        var witnessAccount = DataProvider.TestAccountForWitness;
        WitnessAccountId = witnessAccount.Id;
        DbContext.Accounts.Add(witnessAccount);
        await DbContext.SaveChangesAsync();

        DbContext.ChangeTracker.Clear();
    }

    [TestCleanup]
    public async Task Cleanup()
    {
        await DbContext.Database.EnsureDeletedAsync();
    }

    [TestMethod]
    public async Task WhenUpdating_Moderator_Info()
    {
        var account = await DbContext
            .Accounts
            .Include(e => e.Person!)
            .Include(account => account.Moderator!)
            .FirstOrDefaultAsync(e => e.Id == ModeratorAccountId);
        var person = account!.Person!;
        var moderator = account.Moderator!;
        var newLastName = DataProvider.LastName;
        account.UpdateModerator(
            person.Nic,
            person.FirstName,
            newLastName,
            person.Gender,
            person.BirthDate,
            moderator.PoliceId,
            moderator.City,
            moderator.Province);

        DbContext.Accounts.Update(account);
        await DbContext.SaveChangesAsync();

        account = await DbContext
            .Accounts
            .Include(e => e.Person!)
            .FirstOrDefaultAsync(e => e.Id == ModeratorAccountId);
        Assert.AreEqual(newLastName, account!.Person!.LastName);
    }
}
