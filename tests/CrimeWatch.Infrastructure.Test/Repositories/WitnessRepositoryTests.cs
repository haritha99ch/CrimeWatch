using CrimeWatch.Domain.AggregateModels.WitnessAggregate;

namespace CrimeWatch.Infrastructure.Test.Repositories;
[TestClass]
public class WitnessEntityRepositoryTests : RepositoryTests
{
    private readonly IRepository<Witness, WitnessId> _repository;
    private readonly IApplicationDbContext _dbContext;

    public WitnessEntityRepositoryTests() : base("WitnessTesting")
    {
        _dbContext = GetService<IApplicationDbContext>();
        _repository = GetService<IRepository<Witness, WitnessId>>();
    }

    [TestInitialize]
    public async Task TestInitializeAsync()
    {
        // Add test data to the in-memory database
        var testUser = DataProvider.GetTestUsers().FirstOrDefault()!;
        await _dbContext.User.AddAsync(testUser);
        await _dbContext.SaveChangesAsync();
    }

    [TestCleanup]
    public async Task TestCleanupAsync()
    {
        // Clean up the database after each test
        _dbContext.Witness.RemoveRange(_dbContext.Witness);
        _dbContext.User.RemoveRange(_dbContext.User);
        await _dbContext.SaveChangesAsync();
    }

    [TestMethod]
    public async Task AddAsync_Should_Add_Witness_To_Context()
    {
        // Arrange
        var testUser = await _dbContext.User.FirstOrDefaultAsync();
        var witness = DataProvider.GetTestWitness().FirstOrDefault()!;
        witness.UserId = testUser!.Id;

        // Act
        var addedWitness = await _repository.AddAsync(witness);

        // Assert
        Assert.AreSame(witness, addedWitness);
    }

    [TestMethod]
    public async Task GetByIdAsync_Should_Return_Witness_By_Id()
    {
        // Arrange
        var testUser = await _dbContext.User.FirstOrDefaultAsync();
        var witness = DataProvider.GetTestWitness().FirstOrDefault()!;
        witness.UserId = testUser!.Id;
        await _repository.AddAsync(witness);

        // Act
        var retrievedWitness = await _repository.GetByIdAsync(witness.Id);

        // Assert
        Assert.IsNotNull(retrievedWitness);
        Assert.AreEqual(witness.Id, retrievedWitness.Id);
    }

    [TestMethod]
    public async Task GetAllAsync_Should_Return_All_Witnesses()
    {
        // Arrange
        var testUser = await _dbContext.User.FirstOrDefaultAsync();
        var witness1 = DataProvider.GetTestWitness().FirstOrDefault()!;
        witness1.UserId = testUser!.Id;

        var witness2 = DataProvider.GetTestWitness().LastOrDefault()!;
        witness2.UserId = testUser.Id;

        await _repository.AddAsync(witness1);
        await _repository.AddAsync(witness2);

        // Act
        var witnesses = await _repository.GetAllAsync();

        // Assert
        Assert.AreEqual(2, witnesses.Count);
        Assert.IsTrue(witnesses.Any(w => w.Id == witness1.Id));
        Assert.IsTrue(witnesses.Any(w => w.Id == witness2.Id));
    }

    [TestMethod]
    public async Task CountAsync_Should_Return_Count_Of_Witnesses()
    {
        // Arrange
        var testUser = await _dbContext.User.FirstOrDefaultAsync();
        var witness1 = DataProvider.GetTestWitness().FirstOrDefault()!;
        witness1.UserId = testUser!.Id;

        var witness2 = DataProvider.GetTestWitness().LastOrDefault()!;
        witness2.UserId = testUser.Id;

        await _repository.AddAsync(witness1);
        await _repository.AddAsync(witness2);

        // Act
        var count = await _repository.CountAsync();

        // Assert
        Assert.AreEqual(2, count);
    }

    [TestMethod]
    public async Task DeleteByIdAsync_Should_Delete_Witness_From_Context()
    {
        // Arrange
        var testUser = await _dbContext.User.FirstOrDefaultAsync();
        var witness = DataProvider.GetTestWitness().FirstOrDefault()!;
        witness.UserId = testUser!.Id;
        await _repository.AddAsync(witness);

        // Act
        var isDeleted = await _repository.DeleteByIdAsync(witness.Id);

        // Assert
        Assert.IsTrue(isDeleted);
        var deletedWitness = await _dbContext.Witness.FindAsync(witness.Id);
        Assert.IsNull(deletedWitness);
    }

    [TestMethod]
    public async Task DeleteByIdAsync_Should_Return_False_If_Witness_Not_Found()
    {
        // Act
        var isDeleted = await _repository.DeleteByIdAsync(new(Guid.NewGuid()));

        // Assert
        Assert.IsFalse(isDeleted);
    }
}
