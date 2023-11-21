using Domain.AggregateModels.AccountAggregate.Common;

namespace Domain.AggregateModels.AccountAggregate.ValueObjects;

public sealed record EmailVerificationCode : VerificationCode
{
    private EmailVerificationCode(int value)
        : base(value) { }

    private EmailVerificationCode(int value, DateTime createdAt)
        : base(value, createdAt) { }

    public static EmailVerificationCode Create()
    {
        var value = Random.Shared.Next(100000, 999999);
        return new(value, DateTime.Now);
    }

    /// <summary>
    ///     To generate value object from application layer. Ex: Receive verification code then create the
    ///     AccountVerificationCode before account.VerifyEmail(verificationCode)
    /// </summary>
    /// <param name="value">Verification code received from email or phone</param>
    /// <returns></returns>
    public static EmailVerificationCode Create(int value) => new(value);
}
