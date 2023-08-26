using Bogus;
using CrimeWatch.Domain.AggregateModels.ReportAggregate;
using CrimeWatch.Domain.AggregateModels.WitnessAggregate;

namespace CrimeWatch.Infrastructure.Test.Helpers;
internal static class DataProvider
{
    private static readonly Faker Faker = new();

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

    internal static List<Witness> GetTestWitness()
    {
        return new(){
            Witness.Create(Faker.Name.FirstName(), Faker.Name.LastName(), Gender.Male, new DateOnly(1990, 1, 1), Faker.Phone.PhoneNumber(), Faker.Internet.Email(), "password"),
            Witness.Create(Faker.Name.FirstName(), Faker.Name.LastName(), Gender.Female, new DateOnly(1985, 5, 15), Faker.Phone.PhoneNumber(), Faker.Internet.Email(), "password")
        };
    }
}
