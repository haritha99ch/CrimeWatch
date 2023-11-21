using Domain.AggregateModels.AccountAggregate.ValueObjects;

namespace Domain.Test.AggregateModels.AccountAggregateTests;

[TestClass]
public class WhenVerifyingPhoneNumber
{
    [TestMethod]
    public void ShouldVerify_PhoneNumber()
    {
        var account = DataProvider.TestAccountForModerator;

        account.VerifyPhoneNumber(account.PhoneNumberVerificationCode);

        Assert.IsTrue(account.IsPhoneNumberVerified);
    }

    [TestMethod]
    public void ShouldRaise_AccountPhoneNumberVerifiedEvent()
    {
        var account = DataProvider.TestAccountForModerator;

        account.VerifyPhoneNumber(
            PhoneNumberVerificationCode.Create(account.PhoneNumberVerificationCode.Value)
        );

        Assert.IsTrue(account.HasDomainEvent<AccountPhoneNumberVerifiedEvent>());
    }
}
