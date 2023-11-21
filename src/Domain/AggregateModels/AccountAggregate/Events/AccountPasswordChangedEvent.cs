using Domain.AggregateModels.AccountAggregate.ValueObjects;

namespace Domain.AggregateModels.AccountAggregate.Events;

public record AccountPasswordChangedEvent(AccountId AccountId) : IDomainEvent;
