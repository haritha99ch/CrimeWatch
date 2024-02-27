using Domain.AggregateModels.AccountAggregate;
using Domain.AggregateModels.ReportAggregate;
using Domain.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Test.Reports;
[TestClass]
public class WhenUpdating : TestBase
{
    private Account WitnessAccount { get; set; } = default!;
    private Account ModeratorAccount { get; set; } = default!;
    private List<Report> Reports { get; set; } = new();

    [TestInitialize]
    public async Task Initialize()
    {
        await DbContext.Database.EnsureDeletedAsync();
        await DbContext.Database.EnsureCreatedAsync();

        WitnessAccount = DataProvider.TestAccountForWitness;
        await DbContext.Accounts.AddAsync(WitnessAccount);

        ModeratorAccount = DataProvider.TestAccountForModerator;
        await DbContext.Accounts.AddAsync(ModeratorAccount);

        Reports = Enumerable.Repeat(DataProvider.GetReport(WitnessAccount.Id), 5).ToList();
        await DbContext.Reports.AddRangeAsync(Reports);

        await DbContext.SaveChangesAsync();

        DbContext.ChangeTracker.Clear();
    }

    [TestCleanup]
    public async Task Cleanup()
    {
        await DbContext.Database.EnsureDeletedAsync();
    }

    [TestMethod]
    public async Task ShouldUpdateReport()
    {
        var report = await DbContext
            .Reports
            .Include(e => e.MediaItem)
            .Include(e => e.Location)
            .FirstOrDefaultAsync();
        var newCaption = DataProvider.Caption;
        var newDescription = DataProvider.Description;

        report!.Update(
            newCaption,
            newDescription,
            report.Location.No!,
            report.Location.Street1,
            report.Location.Street2!,
            report.Location.City,
            report.Location.Province,
            report.ViolationTypes,
            report.MediaItem);
        DbContext.Reports.Update(report);
        await DbContext.SaveChangesAsync();

        report = await DbContext.Reports.Include(e => e.MediaItem).FirstOrDefaultAsync();
        Assert.AreEqual(newCaption, report!.Caption);
        Assert.AreEqual(newDescription, report.Description);
    }

    [TestMethod]
    public async Task When_Updating_Relation()
    {
        var report = await DbContext.Reports.FirstOrDefaultAsync();

        report!.SetModerator(ModeratorAccount.Id);
        DbContext.Reports.Update(report);
        await DbContext.SaveChangesAsync();

        report = await DbContext.Reports.Include(e => e.Moderator).FirstOrDefaultAsync();
        Assert.AreEqual(ModeratorAccount.Id, report!.ModeratorId);
    }

    [TestMethod]
    public async Task When_Inserting_OwnedEntity()
    {
        var report = await DbContext.Reports.FirstOrDefaultAsync();

        var caption = DataProvider.Caption;
        var description = DataProvider.Description;
        var no = DataProvider.No;
        var street1 = DataProvider.Street1;
        var street2 = DataProvider.Street2;
        var city = DataProvider.City;
        var province = DataProvider.Province;
        var mediaItems = Enumerable.Repeat(DataProvider.TestMediaItem, 3).ToList();

        report!.AddEvidence(
            WitnessAccount.Id,
            caption,
            description,
            no,
            street1,
            street2,
            city,
            province,
            mediaItems);
        DbContext.Reports.Update(report);
        await DbContext.SaveChangesAsync();

        report = await DbContext.Reports.Include(e => e.Evidences).FirstOrDefaultAsync();
        Assert.AreEqual(1, report!.Evidences.Count);
    }

    [TestMethod]
    public async Task When_Updating_OwnedEntity()
    {
        var report = DataProvider.GetReportWithAEvidence(WitnessAccount.Id);
        await DbContext.Reports.AddAsync(report);
        await DbContext.SaveChangesAsync();
        DbContext.ChangeTracker.Clear();

        report = await DbContext
            .Reports
            .Include(e => e.Evidences)
            .ThenInclude(e => e.MediaItems)
            .FirstOrDefaultAsync(r => r.Id.Equals(report.Id));
        var evidence = report!.Evidences.First();
        var newCaption = DataProvider.Caption;
        var newDescription = DataProvider.Description;
        var newNo = DataProvider.No;
        var newStreet1 = DataProvider.Street1;
        var newStreet2 = DataProvider.Street2;
        var newCity = DataProvider.City;
        var newProvince = DataProvider.Province;
        var newMediaItems = new List<MediaUpload>();
        evidence.MediaItems.Clear(); // Remove all existing media items
        for (var i = 0; i < 3; i++)
        {
            newMediaItems.Add(DataProvider.TestMediaItem);
        }

        report.UpdateEvidence(
            evidence.Id,
            newCaption,
            newDescription,
            newNo,
            newStreet1,
            newStreet2,
            newCity,
            newProvince,
            evidence.MediaItems,
            newMediaItems);
        DbContext.Reports.Update(report);
        await DbContext.SaveChangesAsync();

        report = await DbContext
            .Reports
            .Include(e => e.Evidences)
            .ThenInclude(e => e.MediaItems)
            .FirstOrDefaultAsync(r => r.Id.Equals(report.Id));
        evidence = report!.Evidences.First();
        Assert.AreEqual(newCaption, evidence.Caption);
        Assert.AreEqual(newDescription, evidence.Description);
        Assert.AreEqual(newNo, evidence.Location.No);
        Assert.AreEqual(newStreet1, evidence.Location.Street1);
        Assert.AreEqual(newStreet2, evidence.Location.Street2);
        Assert.AreEqual(newCity, evidence.Location.City);
        Assert.AreEqual(newProvince, evidence.Location.Province);
    }

    [TestMethod]
    public async Task When_Deleting_OwnedEntity()
    {
        var report = DataProvider.GetReportWithAEvidence(WitnessAccount.Id);
        var initialEvidenceCount = report.Evidences.Count;
        await DbContext.Reports.AddAsync(report);
        await DbContext.SaveChangesAsync();
        DbContext.ChangeTracker.Clear();

        report = await DbContext
            .Reports
            .Include(e => e.Evidences)
            .FirstOrDefaultAsync(r => r.Id.Equals(report.Id));
        var evidence = report!.Evidences.First();
        report.RemoveEvidence(evidence.Id);
        DbContext.Reports.Update(report);
        await DbContext.SaveChangesAsync();

        report = await DbContext.Reports.Include(e => e.Evidences).FirstOrDefaultAsync(r => r.Id.Equals(report.Id));
        Assert.AreEqual(initialEvidenceCount - 1, report!.Evidences.Count);
    }
}
