using Bogus;
using CrimeWatch.Domain.AggregateModels.AccountAggregate;
using CrimeWatch.Domain.AggregateModels.ModeratorAggregate;
using CrimeWatch.Domain.AggregateModels.ReportAggregate;
using CrimeWatch.Domain.AggregateModels.UserAggregate;
using CrimeWatch.Domain.AggregateModels.WitnessAggregate;
using CrimeWatch.Domain.Enums;
using CrimeWatch.Domain.ValueObjects;

namespace CrimeWatch.Application.Test.Helpers;
internal static class DataProvider
{
    private static readonly Faker Faker = new();

    internal static List<Moderator> GetTestModerators()
    {
        Faker<Moderator> _faker = new Faker<Moderator>()
            .RuleFor(u => u.Id, f => new(f.Random.Guid()))
            .RuleFor(m => m.PoliceId, f => f.Random.AlphaNumeric(8))
            .RuleFor(m => m.Province, f => f.Address.State())
            .RuleFor(m => m.User, User.Create(Faker.Name.FirstName(), Faker.Name.LastName(), Gender.Female, new DateOnly(1985, 5, 15), Faker.Phone.PhoneNumber()))
            .RuleFor(m => m.Account, Account.Create(Faker.Internet.Email(), "password", true));

        return _faker.Generate(2);
    }

    internal static List<Account> GetTestAccounts()
    {
        Faker<Account> _faker = new Faker<Account>()
            .RuleFor(a => a.Email, f => f.Internet.Email())
            .RuleFor(a => a.Password, f => f.Internet.Password())
            .RuleFor(a => a.IsModerator, f => f.Random.Bool());

        return _faker.Generate(2);
    }

    internal static List<Report> GetTestReports()
    {
        return new()
        {
            Report.Create(
                new WitnessId(Guid.NewGuid()), Faker.Lorem.Sentence(), Faker.Lorem.Paragraph(), Location.Create(
                    null, Faker.Address.StreetAddress(), null, Faker.Address.City(), Faker.Address.State()), MediaItem.Create(
                        MediaItemType.Image, Faker.Image.PicsumUrl())),

            Report.Create(
                new WitnessId(Guid.NewGuid()), Faker.Lorem.Sentence(), Faker.Lorem.Paragraph(), Location.Create(
                    null, Faker.Address.StreetAddress(), null, Faker.Address.City(), Faker.Address.State()), MediaItem.Create(
                        MediaItemType.Image, Faker.Image.PicsumUrl()))
        };
    }

    internal static List<User> GetTestUsers()
    {
        var userFaker = new Faker<User>()
                .RuleFor(u => u.Id, f => new UserId(f.Random.Guid()))
                .RuleFor(u => u.FirstName, f => f.Person.FirstName)
                .RuleFor(u => u.LastName, f => f.Person.LastName)
                .RuleFor(u => u.Gender, f => f.PickRandom<Gender>())
                .RuleFor(u => u.DateOfBirth, f => f.Date.PastDateOnly())
                .RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber());

        return userFaker.Generate(2);
    }

    internal static List<Witness> GetTestWitness()
    {
        Faker<Witness> _faker = new Faker<Witness>()
           .RuleFor(u => u.Id, f => new(f.Random.Guid()))
           .RuleFor(m => m.User, User.Create(Faker.Name.FirstName(), Faker.Name.LastName(), Gender.Female, new DateOnly(1985, 5, 15), Faker.Phone.PhoneNumber()))
           .RuleFor(m => m.Account, Account.Create(Faker.Internet.Email(), "password", true));

        return _faker.Generate(2);
    }

    internal static Location GetTestLocation()
    {
        return Location.Create(
                       null, Faker.Address.StreetAddress(), null, Faker.Address.City(), Faker.Address.State());
    }
}
