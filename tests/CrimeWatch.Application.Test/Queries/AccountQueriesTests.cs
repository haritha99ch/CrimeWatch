using CrimeWatch.Application.Queries.GetAccount;

namespace CrimeWatch.Application.Test.Queries;
[TestClass]
public class AccountQueriesTests : CQRSTests
{
    public AccountQueriesTests() : base("Account") { }

    [TestInitialize]
    public async Task TestInitializeAsync()
    {
        // Add test data to the in-memory database
        var testWitness = DataProvider.GetTestWitness().FirstOrDefault()!;
        var testModerator = DataProvider.GetTestModerators().FirstOrDefault()!;
        await _dbContext.Witness.AddAsync(testWitness);
        await _dbContext.Moderator.AddAsync(testModerator);
        await _dbContext.SaveChangesAsync();
    }

    [TestCleanup]
    public async Task TestCleanupAsync()
    {
        // Clean up the database after each test
        _dbContext.Witness.RemoveRange(_dbContext.Witness);
        _dbContext.Moderator.RemoveRange(_dbContext.Moderator);
        await _dbContext.SaveChangesAsync();
    }

    [TestMethod]
    public async Task GetAccountBySignInQuery_Should_Return_Account_IfExists()
    {
        // Arrange
        var witness = await _dbContext.Witness.Include(e => e.Account).FirstOrDefaultAsync();
        GetAccountBySignInQuery query = new(witness!.Account!.Email, witness!.Account.Password);

        // Act
        var account = await _mediator.Send(query);

        // Assert
        Assert.IsNotNull(account);
        Assert.AreEqual(witness!.Account!.Id, account!.Id);
        Assert.AreEqual(witness!.Account!.Email, account!.Email);
    }
}
