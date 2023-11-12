namespace Domain.AggregateModels.AccountAggregate.Events;
public record AccountUpdatedEvent(Account Account) : IDomainEvent;
