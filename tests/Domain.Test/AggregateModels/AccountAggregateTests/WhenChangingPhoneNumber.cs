namespace Domain.Test.AggregateModels.AccountAggregateTests;

[TestClass]
public class WhenChangingPhoneNumber
{
    [TestMethod]
    public void ShouldChange_PhoneNumber()
    {
        var newPhoneNumber = DataProvider.PhoneNumber;
        var account = DataProvider.TestAccountForModerator;

        account.ChangePhoneNumber(newPhoneNumber);

        Assert.AreEqual(newPhoneNumber, account.PhoneNumber);
    }

    [TestMethod]
    public void ShouldRaise_AccountPhoneNumberChangedEvent()
    {
        var newPhoneNumber = DataProvider.PhoneNumber;
        var account = DataProvider.TestAccountForModerator;

        account.ChangePhoneNumber(newPhoneNumber);

        Assert.IsTrue(account.HasDomainEvent<AccountPhoneNumberChangedEvent>());
    }
}
