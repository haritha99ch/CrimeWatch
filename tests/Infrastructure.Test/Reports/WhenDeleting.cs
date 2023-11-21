using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Test.Reports;

[TestClass]
public class WhenDeleting : TestBase
{
    [TestInitialize]
    public async Task Initialize()
    {
        await DbContext.Database.EnsureDeletedAsync();
        await DbContext.Database.EnsureCreatedAsync();
    }

    [TestCleanup]
    public async Task Cleanup()
    {
        await DbContext.Database.EnsureDeletedAsync();
    }

    [TestMethod]
    public async Task ShouldDeleteReport()
    {
        var witness = DataProvider.TestAccountForWitness;
        var report = DataProvider.GetReportWithAEvidence(witness.Id);
        await DbContext.Accounts.AddAsync(witness);
        await DbContext.Reports.AddAsync(report);
        await DbContext.SaveChangesAsync();
        DbContext.ChangeTracker.Clear();

        await DbContext.Reports.Where(e => e.Id.Equals(report.Id)).ExecuteDeleteAsync();
        await DbContext.SaveChangesAsync();

        var reports = await DbContext.Reports.ToListAsync();
        Assert.AreEqual(0, reports.Count);
    }
}
