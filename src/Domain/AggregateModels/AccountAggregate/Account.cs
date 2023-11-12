using Domain.AggregateModels.AccountAggregate.Entities;
using Domain.AggregateModels.AccountAggregate.Enums;
using Domain.AggregateModels.AccountAggregate.Events;
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
        string phoneNumber)
    {
        var account = new Account
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
        account.RaiseDomainEvent(new AccountCreatedEvent(account));

        return account;
    }

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
        string phoneNumber)
    {
        var account = new Account
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

        account.RaiseDomainEvent(new AccountCreatedEvent(account));
        return account;
    }

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
        var thisUpdated = false;
        var personUpdated = Person!.Update(nic, firstName, lastName, gender, birthDay);
        var moderatorUpdated = Moderator!.Update(policeId, city, province);

        if (!email.Equals(Email) || !password.Equals(Password) || !phoneNumber.Equals(PhoneNumber))
        {
            Email = email;
            Password = password;
            PhoneNumber = phoneNumber;
            thisUpdated = true;
        }

        if (!personUpdated && !moderatorUpdated && !thisUpdated) return;
        UpdatedAt = DateTime.Now;
        RaiseDomainEvent(new AccountUpdatedEvent(this));
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
        var thisUpdated = false;
        var personUpdated = Person!.Update(nic, firstName, lastName, gender, birthDay);
        var witnessUpdated = Witness!.Update();

        UpdatedAt = Person.UpdatedAt > Witness.UpdatedAt ? Person.UpdatedAt : Witness.UpdatedAt;

        if (!email.Equals(Email) || !password.Equals(Password) || !phoneNumber.Equals(PhoneNumber))
        {
            Email = email;
            Password = password;
            PhoneNumber = phoneNumber;
            thisUpdated = true;
        }
        
        if (!personUpdated && !witnessUpdated && !thisUpdated) return;
        UpdatedAt = DateTime.Now;
        RaiseDomainEvent(new AccountUpdatedEvent(this));
    }

}
