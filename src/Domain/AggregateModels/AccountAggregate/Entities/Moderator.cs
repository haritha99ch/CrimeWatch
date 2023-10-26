using Domain.AggregateModels.AccountAggregate.ValueObjects;

namespace Domain.AggregateModels.AccountAggregate.Entities;
public sealed class Moderator : Entity<ModeratorId>
{
    public required string PoliceId { get; init; }
    public required string City { get; init; }
    public required string Province { get; init; }

    public static Moderator Create(string policeId, string city, string province) => new()
    {
        Id = new(Guid.NewGuid()),
        CreatedAt = DateTime.Now,
        PoliceId = policeId,
        City = city,
        Province = province
    };
}
