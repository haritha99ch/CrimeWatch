namespace Domain.Test.AggregateModels.AccountAggregateTests;
[TestClass]
public class WhenChangingEmail
{
    [TestMethod]
    public void ShouldChange_Email()
    {
        var newEmail = DataProvider.Email;
        var account = DataProvider.TestAccountForModerator;

        account.ChangeEmail(newEmail);

        Assert.AreEqual(newEmail, account.Email);
    }

    [TestMethod]
    public void ShouldRaise_AccountEmailChangedEvent()
    {
        var newEmail = DataProvider.Email;
        var account = DataProvider.TestAccountForModerator;

        account.ChangeEmail(newEmail);

        Assert.IsTrue(account.HasDomainEvent<AccountEmailChangedEvent>());
    }
}
