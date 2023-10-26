namespace CrimeWatch.Infrastructure.Test.Repositories;
[TestClass]
public class UserRepositoryTests : RepositoryTests
{
    private readonly IRepository<User, UserId> _repository;
    private readonly IApplicationDbContext _dbContext;

    public UserRepositoryTests() : base("UserTesting")
    {
        _dbContext = GetService<IApplicationDbContext>();
        _repository = GetService<IRepository<User, UserId>>();
    }

    [TestCleanup]
    public async Task TestCleanupAsync()
    {
        // Clean up the database after each test
        _dbContext.User.RemoveRange(_dbContext.User);
        await _dbContext.SaveChangesAsync();
    }

    [TestMethod]
    public async Task AddAsync_Should_Add_User_To_Context()
    {
        // Arrange
        var user = DataProvider.GetTestUsers().First();

        // Act
        var addedUser = await _repository.AddAsync(user);

        // Assert
        Assert.AreSame(user, addedUser);
    }

    [TestMethod]
    public async Task GetByIdAsync_Should_Return_User_By_Id()
    {
        // Arrange
        var user = DataProvider.GetTestUsers().First();
        await _repository.AddAsync(user);

        // Act
        var retrievedUser = await _repository.GetByIdAsync(user.Id);

        // Assert
        Assert.IsNotNull(retrievedUser);
        Assert.AreEqual(user.Id, retrievedUser.Id);
    }

    [TestMethod]
    public async Task GetAllAsync_Should_Return_All_Users()
    {
        // Arrange
        var user1 = DataProvider.GetTestUsers().First();
        var user2 = DataProvider.GetTestUsers().Last();

        await _repository.AddAsync(user1);
        await _repository.AddAsync(user2);

        // Act
        var users = await _repository.GetAllAsync();

        // Assert
        Assert.AreEqual(2, users.Count);
        Assert.IsTrue(users.Any(u => u.Id == user1.Id));
        Assert.IsTrue(users.Any(u => u.Id == user2.Id));
    }

    [TestMethod]
    public async Task CountAsync_Should_Return_Count_Of_Users()
    {
        // Arrange
        var user1 = DataProvider.GetTestUsers().First();
        var user2 = DataProvider.GetTestUsers().Last();

        await _repository.AddAsync(user1);
        await _repository.AddAsync(user2);

        // Act
        var count = await _repository.CountAsync();

        // Assert
        Assert.AreEqual(2, count);
    }

    [TestMethod]
    public async Task UpdateAsync_Should_Update_User_In_Context()
    {
        // Arrange
        var user = DataProvider.GetTestUsers().First();
        user = await _repository.AddAsync(user);

        var updatedUser = user;
        updatedUser.FirstName = "UpdatedFirstName";
        updatedUser.LastName = "UpdatedLastName";

        // Act
        var updatedEntity = await _repository.UpdateAsync(updatedUser);

        // Assert
        Assert.AreEqual(updatedUser.FirstName, updatedEntity.FirstName);
        Assert.AreEqual(updatedUser.LastName, updatedEntity.LastName);
    }

    [TestMethod]
    public async Task DeleteByIdAsync_Should_Delete_User_From_Context()
    {
        // Arrange
        var user = DataProvider.GetTestUsers().First();
        await _repository.AddAsync(user);

        // Act
        var isDeleted = await _repository.DeleteByIdAsync(user.Id);

        // Assert
        Assert.IsTrue(isDeleted);
        var deletedUser = await _dbContext.User.FindAsync(user.Id);
        Assert.IsNull(deletedUser);
    }

    [TestMethod]
    public async Task DeleteByIdAsync_Should_Return_False_If_User_Not_Found()
    {
        // Act
        var isDeleted = await _repository.DeleteByIdAsync(new(Guid.NewGuid()));

        // Assert
        Assert.IsFalse(isDeleted);
    }
}
