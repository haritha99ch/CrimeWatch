using Domain.AggregateModels.AccountAggregate.Entities;
using Domain.AggregateModels.AccountAggregate.Enums;
using Domain.AggregateModels.AccountAggregate.ValueObjects;

namespace Domain.AggregateModels.AccountAggregate;
public sealed record Account : AggregateRoot<AccountId>
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string PhoneNumber { get; set; }
    public required AccountType AccountType { get; init; }

    public Person? Person { get; private init; }
    public Witness? Witness { get; private init; }
    public Moderator? Moderator { get; private init; }

    public static Account CreateAccountForWitness(
        string nic,
        string firstName,
        string lastName,
        Gender gender,
        DateOnly birthDay,
        string email,
        string password,
        string phoneNumber) => new()
    {
        Email = email,
        Password = password,
        PhoneNumber = phoneNumber,
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
        string password,
        string phoneNumber) => new()
    {
        Email = email,
        Password = password,
        PhoneNumber = phoneNumber,
        AccountType = AccountType.Moderator,
        Id = new(Guid.NewGuid()),
        Person = Person.Create(nic, firstName, lastName, gender, birthDay),
        Witness = Witness.Create(),
        Moderator = Moderator.Create(policeId, city, province),
        CreatedAt = DateTime.Now
    };

    public void UpdateModerator(string nic,
        string firstName,
        string lastName,
        Gender gender,
        DateOnly birthDay,
        string policeId,
        string city,
        string province,
        string email,
        string password,
        string phoneNumber)
    {
        Person!.Update(nic, firstName, lastName, gender, birthDay);
        Moderator!.Update(policeId, city, province);

        if (email.Equals(Email) && password.Equals(Password)) return;

        Email = email;
        Password = password;
        PhoneNumber = phoneNumber;
        UpdatedAt = DateTime.Now;
    }

    public void UpdateWitness(string nic,
        string firstName,
        string lastName,
        Gender gender,
        DateOnly birthDay,
        string email,
        string password,
        string phoneNumber)
    {
        Person!.Update(nic, firstName, lastName, gender, birthDay);
        Witness!.Update();

        UpdatedAt = Person.UpdatedAt > Witness.UpdatedAt ? Person.UpdatedAt : Witness.UpdatedAt;

        if (email.Equals(Email) && password.Equals(Password)) return;

        Email = email;
        Password = password;
        PhoneNumber = phoneNumber;
        UpdatedAt = DateTime.Now;
    }

}
