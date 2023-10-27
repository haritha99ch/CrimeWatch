namespace Domain.Common.Models;
public abstract record Entity<TEntityId> : EntityBase where TEntityId : EntityId
{
    public required TEntityId Id { get; init; }
    public required DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; set; }
}
