using Domain.AggregateModels.AccountAggregate.Entities;
using Domain.AggregateModels.AccountAggregate.Enums;
using Domain.AggregateModels.AccountAggregate.ValueObjects;

namespace Domain.AggregateModels.AccountAggregate;
public sealed class Account : AggregateRoot<AccountId>
{
    public required string Email { get; init; }
    public required string Password { get; init; }
    public required AccountType AccountType { get; init; }

    public Person? Person { get; init; }
    public Witness? Witness { get; init; }
    public Moderator? Moderator { get; init; }

    public static Account CreateAccountForWitness(
        string nic,
        string firstName,
        string lastName,
        Gender gender,
        DateOnly birthDay,
        string email,
        string password) => new()
    {
        Email = email,
        Password = password,
        AccountType = AccountType.Witness,
        Id = new(Guid.NewGuid()),
        Person = Person.Create(nic, firstName, lastName, gender, birthDay),
        Witness = Witness.Create(),
        CreatedAt = DateTime.Now
    };

    public static Account CreateAccountForModerator(
        string nic,
        string firstName,
        string lastName,
        Gender gender,
        DateOnly birthDay,
        string policeId,
        string city,
        string province,
        string email,
        string password) => new()
    {
        Email = email,
        Password = password,
        AccountType = AccountType.Moderator,
        Id = new(Guid.NewGuid()),
        Person = Person.Create(nic, firstName, lastName, gender, birthDay),
        Witness = Witness.Create(),
        Moderator = Moderator.Create(policeId, city, province),
        CreatedAt = DateTime.Now
    };

}
