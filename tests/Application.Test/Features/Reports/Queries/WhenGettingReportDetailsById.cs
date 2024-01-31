using Application.Features.Reports.Queries.GetReportDetailsById;

namespace Application.Test.Features.Reports.Queries;
[TestClass]
public class WhenGettingReportDetailsById : TestBase
{
    [TestInitialize]
    public async Task Initialize()
    {
        await InitializeAsync();
    }

    [TestCleanup]
    public async Task Cleanup()
    {
        await CleanupAsync();
    }

    [TestMethod]
    public async Task ShouldReturn_ReportDetails_When_ReportIs_Approved()
    {
        var testWitness = DataProvider.TestAccountForWitness;
        var testReport = DataProvider.GetReport(testWitness.Id);
        testReport.SetApproved();
        await DbContext.Accounts.AddAsync(testWitness);
        await DbContext.Reports.AddAsync(testReport);
        await DbContext.SaveChangesAsync();

        var command = new GetReportDetailsByIdQuery(testReport.Id);
        var result = await Mediator.Send(command);

        var value = result.GetValue();
        Assert.IsNotNull(value);
        Assert.AreEqual(testReport.Id, value.ReportId);
        Assert.AreEqual(testReport.Caption, value.Caption);
        Assert.AreEqual(testReport.Description, value.Description);
        Assert.AreEqual(testReport.Location, value.Location);
        Assert.AreEqual($"{testReport.Author!.Person!.FirstName} {testReport.Author!.Person!.LastName}",
            value.AuthorDetails?.FullName);
        Assert.AreEqual(testReport.Author!.Email, value.AuthorDetails?.Email);
        Assert.AreEqual(testReport.Author!.PhoneNumber, value.AuthorDetails?.PhoneNumber);
    }
}
