using Domain.AggregateModels.AccountAggregate.ValueObjects;

namespace Domain.AggregateModels.AccountAggregate.Events;
public record AccountUpdatedEvent(AccountId AccountId) : IDomainEvent;
