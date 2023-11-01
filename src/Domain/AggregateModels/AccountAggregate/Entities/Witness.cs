using Domain.AggregateModels.AccountAggregate.ValueObjects;

namespace Domain.AggregateModels.AccountAggregate.Entities;
public sealed record Witness : Entity<WitnessId>
{
    public static Witness Create() => new()
    {
        Id = new(Guid.NewGuid()),
        CreatedAt = DateTime.Now
    };

    public Witness Update()
    {
        UpdatedAt = DateTime.Now;
        return this;
    }
}
