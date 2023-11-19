using Infrastructure.Test.Common.Tests;
using TestDataProvider;

namespace Infrastructure.Test.Accounts;
[TestClass]
public class WhenInserting : TestBase
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
    public async Task ShouldInsert_AccountForModerator()
    {
        var accountForModerator = DataProvider.TestAccountForModerator;

        var insertedAccountEntry = await DbContext.Accounts.AddAsync(accountForModerator);
        await DbContext.SaveChangesAsync();
        var insertedAccount = insertedAccountEntry.Entity;

        Assert.IsNotNull(insertedAccount);
        Assert.IsNotNull(insertedAccount.Id);
        Assert.IsNotNull(insertedAccount.Person);
        Assert.IsNotNull(insertedAccount.Person.Id);
        Assert.IsNull(insertedAccount.Witness);
        Assert.IsNotNull(insertedAccount.Moderator);
        Assert.IsNotNull(insertedAccount.Moderator.Id);
        Assert.IsFalse(insertedAccount.IsEmailVerified);
        Assert.IsFalse(insertedAccount.IsPhoneNumberVerified);
        Assert.IsNotNull(insertedAccount.EmailVerificationCode);
        Assert.IsNotNull(insertedAccount.PhoneNumberVerificationCode);
    }

    [TestMethod]
    public async Task ShouldInsert_AccountForWitness()
    {
        var accountForModerator = DataProvider.TestAccountForWitness;

        var insertedAccountEntry = await DbContext.Accounts.AddAsync(accountForModerator);
        await DbContext.SaveChangesAsync();
        var insertedAccount = insertedAccountEntry.Entity;

        Assert.IsNotNull(insertedAccount);
        Assert.IsNotNull(insertedAccount.Id);
        Assert.IsNotNull(insertedAccount.Person);
        Assert.IsNotNull(insertedAccount.Person.Id);
        Assert.IsNull(insertedAccount.Moderator);
        Assert.IsNotNull(insertedAccount.Witness);
        Assert.IsNotNull(insertedAccount.Witness.Id);
        Assert.IsNotNull(insertedAccount.EmailVerificationCode);
        Assert.IsNotNull(insertedAccount.PhoneNumberVerificationCode);
    }
}
