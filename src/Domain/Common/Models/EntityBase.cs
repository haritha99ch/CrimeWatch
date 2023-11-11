namespace Domain.Common.Models;
public abstract record EntityBase
{
    public required DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; set; }
}
