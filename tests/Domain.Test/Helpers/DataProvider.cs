using Bogus;
using Bogus.DataSets;
using Domain.AggregateModels.AccountAggregate;
using Domain.AggregateModels.AccountAggregate.Entities;
using Domain.AggregateModels.AccountAggregate.Enums;
using Person = Bogus.Person;
using PersonEntity = Domain.AggregateModels.AccountAggregate.Entities.Person;

// using Person = Bogus.Person;

namespace Domain.Test.Helpers;
public static class DataProvider
{
    internal static string Nic => new Randomizer().Int(1, 1000000000).ToString();
    internal static string FirstName => new Person().FirstName;
    internal static string LastName => new Person().LastName;
    internal static string PhoneNumber => new Person().Phone;
    internal static DateOnly BirthDate
        => DateOnly.FromDateTime(new Person().DateOfBirth);
    internal static Gender Gender => new Randomizer().Enum<Gender>();
    internal static string Email => new Internet().Email();
    internal static string Password => new Internet().Password();
    internal static string City => new Address().City();
    internal static string Province => new Address().State();
    internal static string PoliceId => new Randomizer().Int(1, 1000000000).ToString();

    internal static Account TestAccountForWitness => GetWitnessAccount();
    internal static Account TestAccountForModerator => GetModeratorAccount();

    private static Account GetWitnessAccount()
    {
        var faker = new Faker<Account>()
            .RuleFor(a => a.Id, () => new(Guid.NewGuid()))
            .RuleFor(a => a.Email, Email)
            .RuleFor(a => a.Password, Password)
            .RuleFor(a => a.PhoneNumber, PhoneNumber)
            .RuleFor(a => a.Person, GetPerson)
            .RuleFor(a => a.Witness, GetWitness)
            .RuleFor(a => a.CreatedAt, DateTime.Now);

        return faker.Generate();
    }

    private static Account GetModeratorAccount()
    {
        var faker = new Faker<Account>()
            .RuleFor(a => a.Id, () => new(Guid.NewGuid()))
            .RuleFor(a => a.Email, Email)
            .RuleFor(a => a.Password, Password)
            .RuleFor(a => a.PhoneNumber, PhoneNumber)
            .RuleFor(a => a.Person, GetPerson)
            .RuleFor(a => a.Moderator, GetModerator)
            .RuleFor(a => a.CreatedAt, DateTime.Now);

        return faker.Generate();
    }

    private static Witness GetWitness()
    {
        var faker = new Faker<Witness>()
            .RuleFor(w => w.Id, () => new(Guid.NewGuid()))
            .RuleFor(w => w.CreatedAt, DateTime.Now);

        return faker.Generate();
    }

    private static Moderator GetModerator()
    {
        var faker = new Faker<Moderator>()
            .RuleFor(m => m.Id, () => new(Guid.NewGuid()))
            .RuleFor(m => m.CreatedAt, DateTime.Now)
            .RuleFor(m => m.PoliceId, PoliceId)
            .RuleFor(m => m.City, City)
            .RuleFor(m => m.Province, Province);

        return faker.Generate();
    }

    private static PersonEntity GetPerson()
    {
        var faker = new Faker<PersonEntity>()
            .RuleFor(p => p.Id, () => new(Guid.NewGuid()))
            .RuleFor(p => p.Nic, Nic)
            .RuleFor(p => p.FirstName, FirstName)
            .RuleFor(p => p.LastName, LastName)
            .RuleFor(p => p.Gender, Gender)
            .RuleFor(p => p.BirthDate, BirthDate)
            .RuleFor(w => w.CreatedAt, DateTime.Now);

        return faker.Generate();
    }
}
