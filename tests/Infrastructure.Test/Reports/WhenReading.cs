using Domain.AggregateModels.AccountAggregate;
using Domain.AggregateModels.ReportAggregate;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Test.Reports;
[TestClass]
public class WhenReading : TestBase
{
    private Account WitnessAccount { get; set; } = default!;
    private List<Report> Reports { get; } = new();
    [TestInitialize]
    public async Task Initialize()
    {
        await DbContext.Database.EnsureDeletedAsync();
        await DbContext.Database.EnsureCreatedAsync();

        WitnessAccount = DataProvider.TestAccountForWitness;
        await DbContext.Accounts.AddAsync(WitnessAccount);

        for (var i = 0; i < 5; i++)
        {
            Reports.Add(DataProvider.GetReport(WitnessAccount.Id));
        }
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
    public async Task When_ReadingById()
    {
        var report = await DbContext.Reports.FindAsync(Reports.First().Id);

        Assert.AreEqual(report!.Id, Reports.First().Id);
    }

    [TestMethod]
    public async Task When_ReadingByPredicating()
    {
        var report = await DbContext.Reports.Where(e => e.Caption.Equals(Reports.First().Caption))
            .SingleOrDefaultAsync();

        Assert.AreEqual(report!.Id, Reports.First().Id);
        Assert.AreEqual(report.Caption, Reports.First().Caption);
    }

    [TestMethod]
    public async Task When_ReadingAll()
    {
        var reports = await DbContext.Reports.ToListAsync();

        Assert.AreEqual(Reports.Count, reports.Count);
    }

    [TestMethod]
    public async Task When_Reading_Including_OwnedEntities()
    {
        var report = await DbContext.Reports.Include(e => e.Author).FirstOrDefaultAsync();

        Assert.IsNotNull(report!.Author);
    }

    [TestMethod]
    public async Task When_Selecting_Properties()
    {
        var report = await DbContext.Reports.Select(e => new { e.Id, e.Caption }).FirstOrDefaultAsync();

        Assert.IsNotNull(report!.Id);
        Assert.IsNotNull(report.Caption);
    }

    [TestMethod]
    public async Task When_Selecting_Properties_From_OwnedEntity()
    {
        var report = await DbContext.Reports.Select(e => new { e.Id, e.Author!.Person!.FirstName })
            .FirstOrDefaultAsync();

        Assert.IsNotNull(report!.Id);
        Assert.IsNotNull(report.FirstName);
    }
}
