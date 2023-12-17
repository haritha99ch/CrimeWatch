using Domain.AggregateModels.AccountAggregate.ValueObjects;

namespace Domain.AggregateModels.AccountAggregate.Events;
public record AccountPhoneNumberChangedEvent(
        AccountId AccountId,
        string PhoneNumber,
        PhoneNumberVerificationCode PhoneNumberVerificationCode
    ) : IDomainEvent;
