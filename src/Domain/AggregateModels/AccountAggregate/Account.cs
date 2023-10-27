using Domain.AggregateModels.AccountAggregate.Entities;
using Domain.AggregateModels.AccountAggregate.Enums;
using Domain.AggregateModels.AccountAggregate.ValueObjects;

namespace Domain.AggregateModels.AccountAggregate;
public sealed record Account : AggregateRoot<AccountId>
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

    public Account UpdateModerator(
        string nic,
        string firstName,
        string lastName,
        Gender gender,
        DateOnly birthDay,
        string policeId,
        string city,
        string province,
        string email,
        string password)
    {
        var updatedPerson = Person!.Update(nic, firstName, lastName, gender, birthDay);
        var updatedModerator = Moderator!.Update(policeId, city, province);

        if (email.Equals(Email)
            && password.Equals(Password)
            && updatedPerson.Equals(Person)
            && updatedModerator.Equals(Moderator)) return this;

        return this with
        {
            Person = updatedPerson,
            Moderator = updatedModerator,
            Email = email,
            Password = password,
            UpdatedAt = DateTime.Now
        };
    }

    public Account UpdateWitness(
        string nic,
        string firstName,
        string lastName,
        Gender gender,
        DateOnly birthDay,
        string email,
        string password)
    {
        var updatedPerson = Person!.Update(nic, firstName, lastName, gender, birthDay);
        var updatedWitness = Witness!.Update();

        if (email.Equals(Email)
            && password.Equals(Password)
            && updatedPerson.Equals(Person)
            && updatedWitness.Equals(Witness)) return this;

        return this with
        {
            Person = updatedPerson,
            Witness = updatedWitness,
            Email = email,
            Password = password,
            UpdatedAt = DateTime.Now
        };
    }

}
