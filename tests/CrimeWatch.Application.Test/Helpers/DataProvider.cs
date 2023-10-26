using Bogus;
using CrimeWatch.Domain.Entities;
using CrimeWatch.Domain.Enums;
using CrimeWatch.Domain.Models;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace CrimeWatch.Application.Test.Helpers;
internal static class DataProvider
{
    private static readonly Faker Faker = new();

    internal static List<Moderator> GetTestModerators()
    {
        var faker = new Faker<Moderator>()
            .RuleFor(u => u.Id, f => new(f.Random.Guid()))
            .RuleFor(m => m.PoliceId, f => f.Random.AlphaNumeric(8))
            .RuleFor(m => m.Province, f => f.Address.State())
            .RuleFor(m => m.User,
                User.Create(Faker.Name.FirstName(), Faker.Name.LastName(), Gender.Female, new(1985, 5, 15),
                    Faker.Phone.PhoneNumber()))
            .RuleFor(m => m.Account, Account.Create(Faker.Internet.Email(), password: "password", true));

        return faker.Generate(2);
    }

    internal static List<Account> GetTestAccounts()
    {
        var faker = new Faker<Account>()
            .RuleFor(a => a.Email, f => f.Internet.Email())
            .RuleFor(a => a.Password, f => f.Internet.Password())
            .RuleFor(a => a.IsModerator, f => f.Random.Bool());

        return faker.Generate(2);
    }

    internal static List<Report> GetTestReports() => new()
    {
        Report.Create(
            new(Guid.NewGuid()), Faker.Lorem.Sentence(), Faker.Lorem.Paragraph(), Location.Create(
                null, Faker.Address.StreetAddress(), null, Faker.Address.City(), Faker.Address.State()),
            MediaItem.Create(
                MediaItemType.Image, Faker.Image.PicsumUrl())!),

        Report.Create(
            new(Guid.NewGuid()), Faker.Lorem.Sentence(), Faker.Lorem.Paragraph(), Location.Create(
                null, Faker.Address.StreetAddress(), null, Faker.Address.City(), Faker.Address.State()),
            MediaItem.Create(
                MediaItemType.Image, Faker.Image.PicsumUrl())!)
    };

    internal static List<User> GetTestUsers()
    {
        var userFaker = new Faker<User>()
            .RuleFor(u => u.Id, f => new(f.Random.Guid()))
            .RuleFor(u => u.FirstName, f => f.Person.FirstName)
            .RuleFor(u => u.LastName, f => f.Person.LastName)
            .RuleFor(u => u.Gender, f => f.PickRandom<Gender>())
            .RuleFor(u => u.DateOfBirth, f => f.Date.Past())
            .RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber());

        return userFaker.Generate(2);
    }

    internal static List<Witness> GetTestWitness()
    {
        var faker = new Faker<Witness>()
            .RuleFor(u => u.Id, f => new(f.Random.Guid()))
            .RuleFor(m => m.User,
                User.Create(Faker.Name.FirstName(), Faker.Name.LastName(), Gender.Female, new(1985, 5, 15),
                    Faker.Phone.PhoneNumber()))
            .RuleFor(m => m.Account, Account.Create(Faker.Internet.Email(), password: "password", false));

        return faker.Generate(2);
    }

    internal static Location GetTestLocation() => Location.Create(
        null, Faker.Address.StreetAddress(), null, Faker.Address.City(), Faker.Address.State());

    internal static List<Evidence> GetTestEvidence()
    {
        var faker = new Faker<Evidence>()
            .RuleFor(e => e.Id, f => new(f.Random.Guid()))
            .RuleFor(e => e.WitnessId, f => new(f.Random.Guid()))
            .RuleFor(e => e.Title, f => f.Lorem.Sentence())
            .RuleFor(e => e.Description, f => f.Lorem.Sentence())
            .RuleFor(e => e.Location, GetTestLocation());

        return faker.Generate(2);
    }

    internal static IFormFile GetTestFile()
    {
        var stream = new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file"));
        var file = new FormFile(stream, 0, stream.Length, name: "Data", fileName: "dummy.png")
        {
            Headers = new HeaderDictionary(),
            ContentType = "image/png"
        };

        return file;
    }
}
