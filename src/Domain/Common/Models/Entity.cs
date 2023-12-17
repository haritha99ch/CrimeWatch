namespace Domain.Common.Models;
public abstract record Entity<TEntityId> : EntityBase
    where TEntityId : EntityId
{
    public required TEntityId Id { get; init; }
}

public abstract record Entity : EntityBase;
