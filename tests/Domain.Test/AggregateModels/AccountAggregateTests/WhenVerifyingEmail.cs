using Domain.AggregateModels.AccountAggregate.ValueObjects;

namespace Domain.Test.AggregateModels.AccountAggregateTests;

[TestClass]
public class WhenVerifyingEmail
{
    [TestMethod]
    public void ShouldVerify_Email()
    {
        var account = DataProvider.TestAccountForModerator;

        account.VerifyEmail(account.EmailVerificationCode);

        Assert.IsTrue(account.IsEmailVerified);
    }

    [TestMethod]
    public void ShouldRaise_AccountEmailVerifiedEvent()
    {
        var account = DataProvider.TestAccountForModerator;

        account.VerifyEmail(EmailVerificationCode.Create(account.EmailVerificationCode.Value));

        Assert.IsTrue(account.HasDomainEvent<AccountEmailVerifiedEvent>());
    }
}
