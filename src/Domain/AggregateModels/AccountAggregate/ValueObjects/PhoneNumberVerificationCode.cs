using Domain.AggregateModels.AccountAggregate.Common;

namespace Domain.AggregateModels.AccountAggregate.ValueObjects;
public record PhoneNumberVerificationCode : VerificationCode
{
    private PhoneNumberVerificationCode(int value, DateTime createdAt) : base(value, createdAt) { }
    private PhoneNumberVerificationCode(int value) : base(value) { }

    public static PhoneNumberVerificationCode Create(int value) => new(value);

    public static PhoneNumberVerificationCode Create()
    {
        var value = Random.Shared.Next(100000, 999999);
        return new(value, DateTime.Now);
    }
}
