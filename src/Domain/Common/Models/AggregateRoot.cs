using Domain.Contracts.Models;

namespace Domain.Common.Models;
public abstract record AggregateRoot<TEntityId> : Entity<TEntityId>, IAggregateRoot where TEntityId : EntityId
{
    public List<IDomainEvent> DomainEvents { get; } = new();

    public void RaiseDomainEvent(IDomainEvent domainEvent) => DomainEvents.Add(domainEvent);
    public void RemoveDomainEvent(IDomainEvent domainEvent) => DomainEvents.Remove(domainEvent);
    public void ClearDomainEvents() => DomainEvents.Clear();
}
