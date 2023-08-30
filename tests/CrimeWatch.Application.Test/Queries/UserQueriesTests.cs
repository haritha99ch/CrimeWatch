using CrimeWatch.Application.Queries.GetModerator;
using CrimeWatch.Application.Queries.GetWitness;

namespace CrimeWatch.Application.Test.Queries;
[TestClass]
public class UserQueriesTests : CQRSTests
{
    public UserQueriesTests() : base("User") { }

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
    public async Task GetWitnessByIdQuery_ShouldReturnCorrectWitness()
    {
        // Arrange
        var witness = await _dbContext.Witness.Include(e => e.Account).FirstOrDefaultAsync();
        GetWitnessByIdQuery query = new(witness!.Id);

        // Act
        var result = await _mediator.Send(query);

        // Assert
        Assert.AreEqual(witness!.Id, result!.Id);
    }

    [TestMethod]
    public async Task GetWitnessByAccountIdQuery_ShouldReturnCorrectWitness()
    {
        // Arrange
        var witness = await _dbContext.Witness.Include(e => e.Account).FirstOrDefaultAsync();
        GetWitnessByAccountIdQuery query = new(witness!.AccountId);

        // Act
        var result = await _mediator.Send(query);

        // Assert
        Assert.AreEqual(witness!.Id, result!.Id);
    }

    [TestMethod]
    public async Task GetModeratorByIdQuery_ShouldReturnCorrectModerator()
    {
        // Arrange
        var moderator = await _dbContext.Moderator.Include(e => e.Account).FirstOrDefaultAsync();
        GetModeratorByIdQuery query = new(moderator!.Id);

        // Act
        var result = await _mediator.Send(query);

        // Assert
        Assert.AreEqual(moderator!.Id, result!.Id);
    }

    [TestMethod]
    public async Task GetModeratorByAccountIdQuery_ShouldReturnCorrectModerator()
    {
        // Arrange
        var moderator = await _dbContext.Moderator.Include(e => e.Account).FirstOrDefaultAsync();
        GetModeratorByAccountIdQuery query = new(moderator!.AccountId);

        // Act
        var result = await _mediator.Send(query);

        // Assert
        Assert.AreEqual(moderator!.Id, result!.Id);
    }
}
