namespace Domain.Common.Models;
public abstract record Entity<TEntityId> : Entity
    where TEntityId : EntityId
{
    public required TEntityId Id { get; init; }
}

public abstract record Entity : EntityBase;
