using Domain.AggregateModels.AccountAggregate.ValueObjects;

namespace Domain.AggregateModels.AccountAggregate.Events;
public record AccountEmailVerifiedEvent(AccountId AccountId, string Email) : IDomainEvent;
