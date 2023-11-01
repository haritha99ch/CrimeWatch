using Domain.AggregateModels.AccountAggregate.Enums;
using Domain.AggregateModels.AccountAggregate.ValueObjects;

namespace Domain.AggregateModels.AccountAggregate.Entities;
public sealed record Person : Entity<PersonId>
{
    public required string Nic { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required DateOnly BirthDate { get; set; }
    public required Gender Gender { get; set; }

    public static Person Create(string nic, string firstName, string lastName, Gender gender, DateOnly birthDate)
        => new()
        {
            Nic = nic,
            FirstName = firstName,
            LastName = lastName,
            BirthDate = birthDate,
            Gender = gender,
            Id = new(Guid.NewGuid()),
            CreatedAt = DateTime.Now
        };

    public Person Update(string nic, string firstName, string lastName, Gender gender, DateOnly birthDate)
    {
        if (nic.Equals(Nic)
            && firstName.Equals(FirstName)
            && lastName.Equals(LastName)
            && gender.Equals(Gender)
            && birthDate.Equals(BirthDate)) return this;

        Nic = nic;
        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
        Gender = gender;
        UpdatedAt = DateTime.Now;

        return this;
    }
}
