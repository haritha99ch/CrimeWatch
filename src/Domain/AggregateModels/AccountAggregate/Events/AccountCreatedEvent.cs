namespace Domain.AggregateModels.AccountAggregate.Events;
public sealed record AccountCreatedEvent(Account Account) : IDomainEvent;
