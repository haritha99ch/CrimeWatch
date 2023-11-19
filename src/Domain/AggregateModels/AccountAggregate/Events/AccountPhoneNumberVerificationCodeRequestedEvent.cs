using Domain.AggregateModels.AccountAggregate.ValueObjects;

namespace Domain.AggregateModels.AccountAggregate.Events;
public sealed record AccountPhoneNumberVerificationCodeRequestedEvent(
    AccountId AccountId,
    PhoneNumberVerificationCode PhoneNumberVerificationCode) : IDomainEvent;
