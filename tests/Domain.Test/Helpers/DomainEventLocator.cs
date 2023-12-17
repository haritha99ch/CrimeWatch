using Domain.Contracts.Models;

namespace Domain.Test.Helpers;
public static class DomainEventLocator
{
    public static bool HasDomainEvent<TDomainEvent>(this IAggregateRoot aggregateRoot)
        where TDomainEvent : IDomainEvent =>
        aggregateRoot.DomainEvents.OfType<TDomainEvent>().Any();
}
