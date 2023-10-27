using Domain.AggregateModels.AccountAggregate.ValueObjects;

namespace Domain.AggregateModels.AccountAggregate.Entities;
public sealed record Moderator : Entity<ModeratorId>
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

    public Moderator Update(string policeId, string city, string province)
    {
        if (policeId.Equals(PoliceId) && city.Equals(City) && province.Equals(Province)) return this;
        return this with
        {
            PoliceId = policeId,
            City = city,
            Province = province,
            UpdatedAt = DateTime.Now
        };
    }
}
