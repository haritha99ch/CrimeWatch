namespace Domain.Common.Models;
public abstract record AggregateRoot<TEntityId> : Entity<TEntityId>, IAggregateRoot where TEntityId : EntityId
{
    public List<IDomainEvent> DomainEvents { get; } = new();

    public void RaiseDomainEvent<TDomainEvent>(TDomainEvent domainEvent) where TDomainEvent : IDomainEvent
        => DomainEvents.Add(domainEvent);

    public void RemoveDomainEvent<TDomainEvent>(TDomainEvent domainEvent) where TDomainEvent : IDomainEvent
        => DomainEvents.Remove(domainEvent);

    public void ClearDomainEvents() => DomainEvents.Clear();
}
