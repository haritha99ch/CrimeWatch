namespace Domain.Contracts.Models;

public interface IAggregateRoot
{
    public List<IDomainEvent> DomainEvents { get; }

    void RaiseDomainEvent<TDomainEvent>(TDomainEvent domainEvent)
        where TDomainEvent : IDomainEvent;
    void RemoveDomainEvent<TDomainEvent>(TDomainEvent domainEvent)
        where TDomainEvent : IDomainEvent;
    void ClearDomainEvents();
}
