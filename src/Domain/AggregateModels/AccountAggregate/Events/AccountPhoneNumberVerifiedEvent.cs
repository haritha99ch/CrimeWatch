using Domain.AggregateModels.AccountAggregate.ValueObjects;

namespace Domain.AggregateModels.AccountAggregate.Events;

public record AccountPhoneNumberVerifiedEvent(AccountId AccountId, string PhoneNumber)
    : IDomainEvent;
