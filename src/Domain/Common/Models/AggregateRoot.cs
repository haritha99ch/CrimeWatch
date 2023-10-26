namespace Domain.Common.Models;
public abstract class AggregateRoot<TEntityId> : Entity<TEntityId> where TEntityId : EntityId;
