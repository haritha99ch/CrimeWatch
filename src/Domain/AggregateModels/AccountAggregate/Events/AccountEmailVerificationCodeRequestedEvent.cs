using Domain.AggregateModels.AccountAggregate.ValueObjects;

namespace Domain.AggregateModels.AccountAggregate.Events;

public record AccountEmailVerificationCodeRequestedEvent(
    AccountId AccountId,
    EmailVerificationCode EmailVerificationCode
) : IDomainEvent;
