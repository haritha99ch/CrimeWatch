using Domain.AggregateModels.AccountAggregate;

namespace Infrastructure.Test.Reports;

[TestClass]
public class WhenInserting : TestBase
{
    private Account WitnessAccount { get; set; } = default!;

    [TestInitialize]
    public async Task Initialize()
    {
        await DbContext.Database.EnsureDeletedAsync();
        await DbContext.Database.EnsureCreatedAsync();

        WitnessAccount = DataProvider.TestAccountForWitness;
        await DbContext.Accounts.AddAsync(WitnessAccount);
        await DbContext.SaveChangesAsync();

        DbContext.ChangeTracker.Clear();
    }

    [TestCleanup]
    public async Task Cleanup()
    {
        await DbContext.Database.EnsureDeletedAsync();
    }

    [TestMethod]
    public async Task ShouldInsert()
    {
        var report = DataProvider.GetReport(WitnessAccount.Id);

        var insertedReportEntry = await DbContext.Reports.AddAsync(report);
        await DbContext.SaveChangesAsync();

        var insertedReport = insertedReportEntry.Entity;

        Assert.IsNotNull(report);
        Assert.IsNotNull(report.MediaItem);
        Assert.AreEqual(insertedReport.AuthorId, WitnessAccount.Id);
        Assert.AreEqual(insertedReport.Caption, report.Caption);
        Assert.AreEqual(insertedReport.Description, report.Description);
        Assert.AreEqual(insertedReport.Location.No, report.Location.No);
        Assert.AreEqual(insertedReport.Location.Street1, report.Location.Street1);
        Assert.AreEqual(insertedReport.Location.Street2, report.Location.Street2);
        Assert.AreEqual(insertedReport.Location.City, report.Location.City);
        Assert.AreEqual(insertedReport.Location.Province, report.Location.Province);
        Assert.AreEqual(insertedReport.ViolationTypes, report.ViolationTypes);
    }
}
