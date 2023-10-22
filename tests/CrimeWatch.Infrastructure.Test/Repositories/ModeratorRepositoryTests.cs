using CrimeWatch.Domain.AggregateModels.ModeratorAggregate;

namespace CrimeWatch.Infrastructure.Test.Repositories;
[TestClass]
public class ModeratorRepositoryTests : RepositoryTests
{
    private readonly IRepository<Moderator, ModeratorId> _repository;
    private readonly IApplicationDbContext _dbContext;

    public ModeratorRepositoryTests() : base("ModeratorTesting")
    {
        _dbContext = GetService<IApplicationDbContext>();
        _repository = GetService<IRepository<Moderator, ModeratorId>>();
    }

    [TestCleanup]
    public async Task TestCleanupAsync()
    {
        // Clean up the database after each test
        _dbContext.Moderator.RemoveRange(_dbContext.Moderator);
        await _dbContext.SaveChangesAsync();
    }

    [TestMethod]
    public async Task AddAsync_Should_Add_Moderator_To_Context()
    {
        // Arrange
        var moderator = DataProvider.GetTestModerators().First();

        // Act
        var addedModerator = await _repository.AddAsync(moderator);

        // Assert
        Assert.AreSame(moderator, addedModerator);
    }

    [TestMethod]
    public async Task GetByIdAsync_Should_Return_Moderator_By_Id()
    {
        // Arrange
        var moderator = DataProvider.GetTestModerators().First();
        await _repository.AddAsync(moderator);

        // Act
        var retrievedModerator = await _repository.GetByIdAsync(moderator.Id);

        // Assert
        Assert.IsNotNull(retrievedModerator);
        Assert.AreEqual(moderator.Id, retrievedModerator.Id);
    }

    [TestMethod]
    public async Task GetAllAsync_Should_Return_All_Moderators()
    {
        // Arrange
        var moderator1 = DataProvider.GetTestModerators().First();
        var moderator2 = DataProvider.GetTestModerators().Last();

        await _repository.AddAsync(moderator1);
        await _repository.AddAsync(moderator2);

        // Act
        var moderators = await _repository.GetAllAsync();

        // Assert
        Assert.AreEqual(2, moderators.Count);
        Assert.IsTrue(moderators.Any(m => m.Id == moderator1.Id));
        Assert.IsTrue(moderators.Any(m => m.Id == moderator2.Id));
    }

    [TestMethod]
    public async Task CountAsync_Should_Return_Count_Of_Moderators()
    {
        // Arrange
        var moderator1 = DataProvider.GetTestModerators().First();
        var moderator2 = DataProvider.GetTestModerators().Last();

        await _repository.AddAsync(moderator1);
        await _repository.AddAsync(moderator2);

        // Act
        var count = await _repository.CountAsync();

        // Assert
        Assert.AreEqual(2, count);
    }

    [TestMethod]
    public async Task UpdateAsync_Should_Update_Moderator_In_Context()
    {
        // Arrange
        var testModerator = DataProvider.GetTestModerators().First();
        await _repository.AddAsync(testModerator);

        var updatedModerator = testModerator;
        updatedModerator.Province = "Updated Province";

        // Act
        var updatedEntity = await _repository.UpdateAsync(updatedModerator);

        // Assert
        Assert.AreEqual(updatedModerator.Province, updatedEntity.Province);
    }

    [TestMethod]
    public async Task DeleteByIdAsync_Should_Delete_Moderator_From_Context()
    {
        // Arrange
        var testModerator = DataProvider.GetTestModerators().First();
        await _repository.AddAsync(testModerator);

        // Act
        var isDeleted = await _repository.DeleteByIdAsync(testModerator.Id);

        // Assert
        Assert.IsTrue(isDeleted);
        var deletedModerator = await _dbContext.Moderator.FindAsync(testModerator.Id);
        Assert.IsNull(deletedModerator);
    }

    [TestMethod]
    public async Task DeleteByIdAsync_Should_Return_False_If_Moderator_Not_Found()
    {
        // Act
        var isDeleted = await _repository.DeleteByIdAsync(new(Guid.NewGuid()));

        // Assert
        Assert.IsFalse(isDeleted);
    }

}
