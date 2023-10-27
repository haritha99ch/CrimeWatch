namespace Domain.Common.Models;
public abstract record AggregateRoot<TEntityId> : Entity<TEntityId> where TEntityId : EntityId;
