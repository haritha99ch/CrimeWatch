using CrimeWatch.Domain.AggregateModels.AccountAggregate;

namespace CrimeWatch.Domain.Test.AggregateModels;
[TestClass]
public class AccountTests
{
    [TestMethod]
    public void AccountCreate_ReturnsValidAccount()
    {
        // Arrange
        var email = "test@example.com";
        var password = "password123";
        var isModerator = true;

        // Act
        var account = Account.Create(email, password, isModerator)!;

        // Assert
        Assert.IsNotNull(account);
        Assert.AreEqual(email, account.Email);
        Assert.AreEqual(password, account.Password);
        Assert.AreEqual(isModerator, account.IsModerator);
    }
}
