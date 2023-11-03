﻿using Bogus;
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
    internal static MediaType MediaType => new Randomizer().Enum<MediaType>();
    internal static string Email => new Internet().Email();
    internal static string Password => new Internet().Password();
    internal static string City => new Address().City();
    internal static string Province => new Address().State();
    internal static string PoliceId => new Randomizer().Int(1, 1000000000).ToString();
    internal static string No => new Address().BuildingNumber();
    internal static string Street1 => new Address().StreetName();
    internal static string Street2 => new Address().StreetName();
    internal static string Caption => new Lorem().Sentence();
    internal static string Description => new Lorem().Paragraphs();
    internal static string MediaItemUrl => new Internet().Url();
    internal static AccountId AuthorId => new(Guid.NewGuid());
    internal static AccountId ModeratorId => new(Guid.NewGuid());
    internal static Account TestAccountForWitness => GetWitnessAccount();
    internal static Account TestAccountForModerator => GetModeratorAccount();
    internal static MediaUpload TestMediaItem => GetMediaUpload();
    internal static Report TestReport => GetReport();
    internal static Report TestModeratedReport => GetModeratedReport();
    internal static Report TestApprovedReport => GetApprovedReport();
    internal static Report TestReportWithAComment => GetReportWithAComment();
    internal static Evidence TestEvidence => GetEvidence();
    internal static Report TestReportWithAEvidence => GetReportWithAEvidence();
    internal static Report TestReportWithAEvidenceIncludingComment => GetReportWithAEvidenceIncludingComment();

    private static Comment TestComment => GetComment();
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

    private static Report GetReport()
    {
        var faker = new Faker<Report>()
            .RuleFor(r => r.Id, () => new(Guid.NewGuid()))
            .RuleFor(r => r.AuthorId, () => new(Guid.NewGuid()))
            .RuleFor(r => r.Caption, Caption)
            .RuleFor(r => r.Status, Status.Pending)
            .RuleFor(r => r.Description, Description)
            .RuleFor(r => r.Location, GetLocation)
            .RuleFor(r => r.MediaItem, GetMediaItem)
            .RuleFor(r => r.CreatedAt, DateTime.Now);

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
            .RuleFor(r => r.Comments, (_, r) =>
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
            .RuleFor(m => m.Url, MediaItemUrl)
            .RuleFor(m => m.MediaType, MediaType);
        return faker.Generate();
    }

    private static MediaItem GetMediaItem()
    {
        var faker = new Faker<MediaItem>()
            .RuleFor(m => m.Id, () => new(Guid.NewGuid()))
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

    private static Evidence GetEvidence()
    {
        var faker = new Faker<Evidence>()
            .RuleFor(e => e.Id, () => new(Guid.NewGuid()))
            .RuleFor(e => e.AuthorId, AuthorId)
            .RuleFor(e => e.Caption, Caption)
            .RuleFor(e => e.Description, Description)
            .RuleFor(e => e.Location, GetLocation)
            .RuleFor(e => e.MediaItems, (_, e) =>
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
            .RuleFor(e => e.MediaItems, (_, e) =>
            {
                for (var i = 0; i < 5; i++)
                {
                    e.MediaItems.Add(GetMediaItem());
                }
                return e.MediaItems;
            })
            .RuleFor(e => e.CreatedAt, DateTime.Now)
            .RuleFor(r => r.Comments, (_, r) =>
            {
                r.Comments.Add(TestComment);
                return r.Comments;
            });

        return faker.Generate();
    }

    private static Report GetReportWithAEvidence()
    {
        var faker = new Faker<Report>()
            .RuleFor(r => r.Id, () => new(Guid.NewGuid()))
            .RuleFor(r => r.AuthorId, () => new(Guid.NewGuid()))
            .RuleFor(r => r.Caption, Caption)
            .RuleFor(r => r.Description, Description)
            .RuleFor(r => r.Location, GetLocation)
            .RuleFor(r => r.MediaItem, GetMediaItem)
            .RuleFor(r => r.CreatedAt, DateTime.Now)
            .RuleFor(r => r.Evidences, (_, r) =>
            {
                r.Evidences.Add(TestEvidence);
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
            .RuleFor(r => r.Evidences, (_, r) =>
            {
                r.Evidences.Add(GetEvidenceWithComment());
                return r.Evidences;
            });

        return faker.Generate();
    }
}
