namespace Domain.Test.AggregateModels.AccountAggregateTests;
[TestClass]
public class WhenRequestingPhoneNumberVerificationCode
{
    [TestMethod]
    public void ShouldUpdate_And_Return_PhoneNumberVerificationCode()
    {
        var account = DataProvider.TestAccountForModerator;
        var currentVerificationCode = account.PhoneNumberVerificationCode;

        var newVerificationCode = account.RequestPhoneNumberVerificationCode();

        Assert.IsFalse(currentVerificationCode.Equals(newVerificationCode));
    }

    [TestMethod]
    public void When_Already_Verified_ShouldUpdate_IsPhoneNumberVerified_And_Return_PhoneNumberVerificationCode()
    {
        var account = DataProvider.TestAccountForModerator;
        var currentVerificationCode = account.EmailVerificationCode;
        account.VerifyEmail(currentVerificationCode);

        var newVerificationCode = account.RequestEmailVerificationCode();

        Assert.IsFalse(currentVerificationCode.Equals(newVerificationCode));
        Assert.IsFalse(account.IsEmailVerified);
    }
    
    [TestMethod]
    public void ShouldRaise_AccountPhoneNumberVerificationCodeRequestedEvent()
    {
        var account = DataProvider.TestAccountForModerator;

        account.RequestPhoneNumberVerificationCode();
        
        Assert.IsTrue(account.HasDomainEvent<AccountPhoneNumberVerificationCodeRequestedEvent>());
    }
}
