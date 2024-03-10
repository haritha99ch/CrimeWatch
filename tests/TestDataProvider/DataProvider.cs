using Bogus;
using Bogus.DataSets;
using Domain.AggregateModels.AccountAggregate;
using Domain.AggregateModels.AccountAggregate.Entities;
using Domain.AggregateModels.AccountAggregate.Enums;
using Domain.AggregateModels.AccountAggregate.ValueObjects;
using Domain.AggregateModels.ReportAggregate;
using Domain.AggregateModels.ReportAggregate.Entities;
using Domain.AggregateModels.ReportAggregate.Enums;
using Domain.AggregateModels.ReportAggregate.ValueObjects;
using Domain.Common.Models;
using Microsoft.AspNetCore.Http;
using Person = Bogus.Person;
using PersonEntity = Domain.AggregateModels.AccountAggregate.Entities.Person;

// using Person = Bogus.Person;

namespace TestDataProvider;
public static class DataProvider
{
    public static string Nic => new Randomizer().Int(1, 1000000000).ToString();
    public static string FirstName => new Person().FirstName;
    public static string LastName => new Person().LastName;
    public static string PhoneNumber =>
        new Randomizer().Long(100000000000, 999999999999).ToString();
    public static DateOnly BirthDate => DateOnly.FromDateTime(new Person().DateOfBirth);
    public static Gender Gender => new Randomizer().Enum<Gender>();
    public static MediaType MediaType => new Randomizer().Enum<MediaType>();
    public static string Email => new Internet().Email();
    public static string Password => new Internet().Password();
    public static string City => new Address().City();
    public static string Province => new Address().State();
    public static string PoliceId => new Randomizer().Int(1, 1000000000).ToString();
    public static string No => new Address().BuildingNumber();
    public static string Street1 => new Address().StreetName();
    public static string Street2 => new Address().StreetName();
    public static List<ViolationType> ViolationTypes =>
        Enumerable.Repeat(new Randomizer().Enum<ViolationType>(), 4).ToList();
    public static string Caption => new Lorem().Sentence();
    public static string Description => new Lorem().Paragraphs();
    public static string MediaItemUrl => new Internet().Url();
    public static IFormFile File => new MockFormFile(Array.Empty<byte>(), new Randomizer().Enum<MediaType>());
    public static AccountId AccountId => new(Guid.NewGuid());
    public static AccountId AuthorId => AccountId;
    public static AccountId ModeratorId => AccountId;
    public static Account TestAccountForWitness => GetWitnessAccount();
    public static Account TestAccountForModerator => GetModeratorAccount();
    public static MediaUpload TestMediaItem => GetMediaUpload();
    public static Report TestReport => GetReport();
    public static Report TestModeratedReport => GetModeratedReport();
    public static Report TestApprovedReport => GetApprovedReport();
    public static Report TestReportWithAComment => GetReportWithAComment();
    public static Report TestReportWithABookmark => GetReportWithBookmark();
    public static Evidence TestEvidence => GetEvidence();
    public static Report TestReportWithAEvidence => GetReportWithAEvidence();
    public static Report TestReportWithAEvidenceIncludingComment =>
        GetReportWithAEvidenceIncludingComment();
    public static EmailVerificationCode TestEmailVerificationCode => GetEmailVerificationCode();
    public static PhoneNumberVerificationCode TestPhoneNumberVerificationCode =>
        GetPhoneNumberVerificationCode();
    public static string TestSecretKey = new Faker().Random.String2(32);
    private static Comment TestComment => GetComment();
    private static Bookmark TestBookmark => GetBookmark();

    private static EmailVerificationCode GetEmailVerificationCode() =>
        EmailVerificationCode.Create();

    private static PhoneNumberVerificationCode GetPhoneNumberVerificationCode() =>
        PhoneNumberVerificationCode.Create();

    private static Bookmark GetBookmark()
    {
        var faker = new Faker<Bookmark>()
            .RuleFor(b => b.Id, () => new(Guid.NewGuid()))
            .RuleFor(b => b.AccountId, () => new(Guid.NewGuid()))
            .RuleFor(a => a.CreatedAt, DateTime.Now);

        return faker.Generate();
    }

    private static Account GetWitnessAccount()
    {
        var faker = new Faker<Account>()
            .RuleFor(a => a.Id, () => new(Guid.NewGuid()))
            .RuleFor(a => a.Email, Email)
            .RuleFor(a => a.Password, BCrypt.Net.BCrypt.HashPassword(Password))
            .RuleFor(a => a.PhoneNumber, PhoneNumber)
            .RuleFor(a => a.AccountType, AccountType.Witness)
            .RuleFor(a => a.Person, GetPerson)
            .RuleFor(a => a.Witness, GetWitness)
            .RuleFor(a => a.IsEmailVerified, false)
            .RuleFor(a => a.IsPhoneNumberVerified, false)
            .RuleFor(a => a.EmailVerificationCode, TestEmailVerificationCode)
            .RuleFor(a => a.PhoneNumberVerificationCode, TestPhoneNumberVerificationCode)
            .RuleFor(a => a.CreatedAt, DateTime.Now);

        return faker.Generate();
    }

    public static Account GetModeratorAccount(string? password = null)
    {
        var faker = new Faker<Account>()
            .RuleFor(a => a.Id, () => new(Guid.NewGuid()))
            .RuleFor(a => a.Email, Email)
            .RuleFor(a => a.Password, BCrypt.Net.BCrypt.HashPassword(password ?? Password))
            .RuleFor(a => a.PhoneNumber, PhoneNumber)
            .RuleFor(a => a.AccountType, AccountType.Moderator)
            .RuleFor(a => a.Person, GetPerson)
            .RuleFor(a => a.Moderator, GetModerator)
            .RuleFor(a => a.IsEmailVerified, false)
            .RuleFor(a => a.IsPhoneNumberVerified, false)
            .RuleFor(a => a.EmailVerificationCode, TestEmailVerificationCode)
            .RuleFor(a => a.PhoneNumberVerificationCode, TestPhoneNumberVerificationCode)
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

    public static Report GetReport(AccountId? accountId = null)
    {
        var faker = new Faker<Report>()
            .RuleFor(r => r.Id, () => new(Guid.NewGuid()))
            .RuleFor(r => r.AuthorId, () => accountId ?? new(Guid.NewGuid()))
            .RuleFor(r => r.Caption, Caption)
            .RuleFor(r => r.Status, Status.Pending)
            .RuleFor(r => r.Description, Description)
            .RuleFor(r => r.Location, GetLocation)
            .RuleFor(r => r.MediaItem, GetMediaItem)
            .RuleFor(r => r.CreatedAt, DateTime.Now);

        return faker.Generate();
    }

    private static Report GetReportWithBookmark()
    {
        var faker = new Faker<Report>()
            .RuleFor(r => r.Id, () => new(Guid.NewGuid()))
            .RuleFor(r => r.AuthorId, () => new(Guid.NewGuid()))
            .RuleFor(r => r.Caption, Caption)
            .RuleFor(r => r.Status, Status.Pending)
            .RuleFor(r => r.Description, Description)
            .RuleFor(r => r.Location, GetLocation)
            .RuleFor(r => r.MediaItem, GetMediaItem)
            .RuleFor(r => r.CreatedAt, DateTime.Now)
            .RuleFor(
                r => r.Bookmarks,
                (_, r) =>
                {
                    r.Bookmarks.Add(TestBookmark);
                    return r.Bookmarks;
                })
            .RuleFor(r => r.BookmarksCount, 1);

        return faker.Generate();
    }

    private static Report GetReportWithAComment()
    {
        var faker = new Faker<Report>()
            .RuleFor(r => r.Id, () => new(Guid.NewGuid()))
            .RuleFor(r => r.AuthorId, () => new(Guid.NewGuid()))
            .RuleFor(r => r.Caption, Caption)
            .RuleFor(r => r.Status, Status.Pending)
            .RuleFor(r => r.Description, Description)
            .RuleFor(r => r.Location, GetLocation)
            .RuleFor(r => r.MediaItem, GetMediaItem)
            .RuleFor(r => r.CreatedAt, DateTime.Now)
            .RuleFor(
                r => r.Comments,
                (_, r) =>
                {
                    r.Comments.Add(TestComment);
                    return r.Comments;
                });

        return faker.Generate();
    }

    private static Report GetModeratedReport()
    {
        var faker = new Faker<Report>()
            .RuleFor(r => r.Id, () => new(Guid.NewGuid()))
            .RuleFor(r => r.AuthorId, () => new(Guid.NewGuid()))
            .RuleFor(r => r.ModeratorId, () => new(Guid.NewGuid()))
            .RuleFor(r => r.Status, Status.UnderReview)
            .RuleFor(r => r.Caption, Caption)
            .RuleFor(r => r.Description, Description)
            .RuleFor(r => r.Location, GetLocation)
            .RuleFor(r => r.MediaItem, GetMediaItem)
            .RuleFor(r => r.CreatedAt, DateTime.Now);

        return faker.Generate();
    }

    private static Report GetApprovedReport()
    {
        var faker = new Faker<Report>()
            .RuleFor(r => r.Id, () => new(Guid.NewGuid()))
            .RuleFor(r => r.AuthorId, () => new(Guid.NewGuid()))
            .RuleFor(r => r.ModeratorId, () => new(Guid.NewGuid()))
            .RuleFor(r => r.Caption, Caption)
            .RuleFor(r => r.Description, Description)
            .RuleFor(r => r.Location, GetLocation)
            .RuleFor(r => r.MediaItem, GetMediaItem)
            .RuleFor(r => r.Status, Status.Approved)
            .RuleFor(r => r.CreatedAt, DateTime.Now);

        return faker.Generate();
    }

    private static Location GetLocation()
    {
        var faker = new Faker<Location>()
            .RuleFor(l => l.No, No)
            .RuleFor(l => l.Street1, Street1)
            .RuleFor(l => l.Street2, Street2)
            .RuleFor(l => l.City, City)
            .RuleFor(l => l.Province, Province);

        return faker.Generate();
    }

    private static MediaUpload GetMediaUpload()
    {
        var faker = new Faker<MediaUpload>()
            .RuleFor(m => m.FileName, value: "FileName.png")
            .RuleFor(m => m.Url, MediaItemUrl)
            .RuleFor(m => m.MediaType, MediaType);
        return faker.Generate();
    }

    private static MediaItem GetMediaItem()
    {
        var faker = new Faker<MediaItem>()
            .RuleFor(m => m.Id, () => new(Guid.NewGuid()))
            .RuleFor(m => m.FileName, value: "FileName.png")
            .RuleFor(m => m.Url, MediaItemUrl)
            .RuleFor(m => m.MediaType, MediaType)
            .RuleFor(m => m.CreatedAt, DateTime.Now);

        return faker.Generate();
    }

    private static Comment GetComment()
    {
        var faker = new Faker<Comment>()
            .RuleFor(c => c.Id, () => new(Guid.NewGuid()))
            .RuleFor(c => c.AuthorId, () => new(Guid.NewGuid()))
            .RuleFor(c => c.Content, Description)
            .RuleFor(c => c.CreatedAt, DateTime.Now);

        return faker.Generate();
    }

    public static Evidence GetEvidence(AccountId? authorId = null)
    {
        var faker = new Faker<Evidence>()
            .RuleFor(e => e.Id, () => new(Guid.NewGuid()))
            .RuleFor(e => e.AuthorId, authorId ?? AuthorId)
            .RuleFor(e => e.Caption, Caption)
            .RuleFor(e => e.Description, Description)
            .RuleFor(e => e.Location, GetLocation)
            .RuleFor(
                e => e.MediaItems,
                (_, e) =>
                {
                    for (var i = 0; i < 5; i++)
                    {
                        e.MediaItems.Add(GetMediaItem());
                    }
                    return e.MediaItems;
                })
            .RuleFor(e => e.CreatedAt, DateTime.Now);

        return faker.Generate();
    }

    private static Evidence GetEvidenceWithComment()
    {
        var faker = new Faker<Evidence>()
            .RuleFor(e => e.Id, () => new(Guid.NewGuid()))
            .RuleFor(e => e.AuthorId, AuthorId)
            .RuleFor(e => e.Caption, Caption)
            .RuleFor(e => e.Description, Description)
            .RuleFor(e => e.Location, GetLocation)
            .RuleFor(
                e => e.MediaItems,
                (_, e) =>
                {
                    for (var i = 0; i < 5; i++)
                    {
                        e.MediaItems.Add(GetMediaItem());
                    }
                    return e.MediaItems;
                })
            .RuleFor(e => e.CreatedAt, DateTime.Now)
            .RuleFor(
                r => r.Comments,
                (_, r) =>
                {
                    r.Comments.Add(TestComment);
                    return r.Comments;
                });

        return faker.Generate();
    }

    public static Report GetReportWithAEvidence(AccountId? authorId = null)
    {
        var faker = new Faker<Report>()
            .RuleFor(r => r.Id, () => new(Guid.NewGuid()))
            .RuleFor(r => r.AuthorId, () => authorId ?? new(Guid.NewGuid()))
            .RuleFor(r => r.Caption, Caption)
            .RuleFor(r => r.Description, Description)
            .RuleFor(r => r.Location, GetLocation)
            .RuleFor(r => r.MediaItem, GetMediaItem)
            .RuleFor(r => r.CreatedAt, DateTime.Now)
            .RuleFor(
                r => r.Evidences,
                (_, r) =>
                {
                    r.Evidences.AddRange(Enumerable.Range(1, 3).Select(_ => GetEvidence(authorId)));
                    return r.Evidences;
                });

        return faker.Generate();
    }

    private static Report GetReportWithAEvidenceIncludingComment()
    {
        var faker = new Faker<Report>()
            .RuleFor(r => r.Id, () => new(Guid.NewGuid()))
            .RuleFor(r => r.AuthorId, () => new(Guid.NewGuid()))
            .RuleFor(r => r.Caption, Caption)
            .RuleFor(r => r.Description, Description)
            .RuleFor(r => r.Location, GetLocation)
            .RuleFor(r => r.MediaItem, GetMediaItem)
            .RuleFor(r => r.CreatedAt, DateTime.Now)
            .RuleFor(
                r => r.Evidences,
                (_, r) =>
                {
                    r.Evidences.Add(GetEvidenceWithComment());
                    return r.Evidences;
                });

        return faker.Generate();
    }
}
