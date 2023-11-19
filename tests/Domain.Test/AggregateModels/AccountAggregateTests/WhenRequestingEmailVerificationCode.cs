namespace Domain.Test.AggregateModels.AccountAggregateTests;
[TestClass]
public class WhenRequestingEmailVerificationCode
{
    [TestMethod]
    public void ShouldUpdate_And_Return_EmailVerificationCode()
    {
        var account = DataProvider.TestAccountForModerator;
        var currentVerificationCode = account.EmailVerificationCode;

        var newVerificationCode = account.RequestEmailVerificationCode();

        Assert.IsFalse(currentVerificationCode.Equals(newVerificationCode));
    }

    [TestMethod]
    public void When_Already_Verified_ShouldUpdate_IsEmailVerified_And_Return_EmailVerificationCode()
    {
        var account = DataProvider.TestAccountForModerator;
        var currentVerificationCode = account.EmailVerificationCode;
        account.VerifyEmail(currentVerificationCode);

        var newVerificationCode = account.RequestEmailVerificationCode();

        Assert.IsFalse(currentVerificationCode.Equals(newVerificationCode));
        Assert.IsFalse(account.IsEmailVerified);
    }

    [TestMethod]
    public void ShouldRaise_AccountEmailVerificationCodeRequestedEvent()
    {
        var account = DataProvider.TestAccountForModerator;

        account.RequestEmailVerificationCode();
        
        Assert.IsTrue(account.HasDomainEvent<AccountEmailVerificationCodeRequestedEvent>());
    }
}
