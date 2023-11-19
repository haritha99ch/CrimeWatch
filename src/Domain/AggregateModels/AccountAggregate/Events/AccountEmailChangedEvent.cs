using Domain.AggregateModels.AccountAggregate.Common;
using Domain.AggregateModels.AccountAggregate.ValueObjects;

namespace Domain.AggregateModels.AccountAggregate.Events;
public record AccountEmailChangedEvent(
    AccountId AccountId,
    string Email,
    VerificationCode VerificationCode) : IDomainEvent;
