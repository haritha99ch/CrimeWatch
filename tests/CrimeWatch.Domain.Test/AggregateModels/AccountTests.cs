using CrimeWatch.Domain.AggregateModels.AccountAggregate;

namespace CrimeWatch.Domain.Test.AggregateModels;
[TestClass]
public class AccountTests
{
    [TestMethod]
    public void AccountCreate_ReturnsValidAccount()
    {
        // Arrange
        string email = "test@example.com";
        string password = "password123";
        bool isModerator = true;

        // Act
        Account account = Account.Create(email, password, isModerator);

        // Assert
        Assert.IsNotNull(account);
        Assert.AreEqual(email, account.Email);
        Assert.AreEqual(password, account.Password);
        Assert.AreEqual(isModerator, account.IsModerator);
    }
}