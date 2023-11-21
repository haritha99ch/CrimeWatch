namespace Domain.Test.AggregateModels.AccountAggregateTests;

[TestClass]
public class WhenChangingPassword
{
    [TestMethod]
    public void ShouldChange_Password()
    {
        var newPassword = DataProvider.Password;
        var account = DataProvider.TestAccountForModerator;

        account.ChangePassword(newPassword);

        Assert.IsTrue(BCrypt.Net.BCrypt.Verify(newPassword, account.Password));
    }

    [TestMethod]
    public void ShouldRaise_AccountPasswordChangedEvent()
    {
        var newPassword = DataProvider.Password;
        var account = DataProvider.TestAccountForModerator;

        account.ChangePassword(newPassword);

        Assert.IsTrue(account.HasDomainEvent<AccountPasswordChangedEvent>());
    }
}
