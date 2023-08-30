using CrimeWatch.Domain.AggregateModels.ReportAggregate;
using CrimeWatch.Domain.AggregateModels.WitnessAggregate;

namespace CrimeWatch.Infrastructure.Test.Repositories;
[TestClass]
public class ReportRepositoryTests : RepositoryTests
{
    private readonly IRepository<Report, ReportId> _repository;
    private readonly IApplicationDbContext _dbContext;

    public ReportRepositoryTests() : base("ReportTesting")
    {
        _dbContext = GetService<IApplicationDbContext>();
        _repository = GetService<IRepository<Report, ReportId>>();
    }

    [TestInitialize]
    public async Task TestInitializeAsync()
    {
        // Add test data to the in-memory database
        Witness testWitness = DataProvider.GetTestWitness().FirstOrDefault()!;
        await _dbContext.Witness.AddAsync(testWitness);
        await _dbContext.SaveChangesAsync();
    }

    [TestCleanup]
    public async Task TestCleanupAsync()
    {
        // Clean up the database after each test
        _dbContext.Witness.RemoveRange(_dbContext.Witness);
        _dbContext.Report.RemoveRange(_dbContext.Report);
        await _dbContext.SaveChangesAsync();
    }

    [TestMethod]
    public async Task AddAsync_Should_Add_Report_To_Context()
    {
        // Arrange
        Witness? testWitness = await _dbContext.Witness.FirstOrDefaultAsync();
        Report report = DataProvider.GetTestReports().FirstOrDefault()!;
        report.WitnessId = testWitness!.Id;

        // Act
        Report? addedReport = await _repository.AddAsync(report);

        // Assert
        Assert.AreSame(report, addedReport);
    }


    [TestMethod]
    public async Task CountAsync_Should_Return_Count_Of_Reports()
    {
        // Arrange
        Witness? testWitness = await _dbContext.Witness.FirstOrDefaultAsync();
        Report report1 = DataProvider.GetTestReports().FirstOrDefault()!;
        report1.WitnessId = testWitness!.Id;

        Report report2 = DataProvider.GetTestReports().FirstOrDefault()!;
        report2.WitnessId = testWitness!.Id;

        await _repository.AddAsync(report1);
        await _repository.AddAsync(report2);

        // Act
        var count = await _repository.CountAsync();

        // Assert
        Assert.AreEqual(2, count);
    }

    [TestMethod]
    public async Task DeleteByIdAsync_Should_Delete_Report_From_Context()
    {
        // Arrange
        Witness? testWitness = await _dbContext.Witness.FirstOrDefaultAsync();
        Report report = DataProvider.GetTestReports().FirstOrDefault()!;
        report.WitnessId = testWitness!.Id;
        await _repository.AddAsync(report);

        // Act
        var isDeleted = await _repository.DeleteByIdAsync(report.Id);

        // Assert
        Assert.IsTrue(isDeleted);
        var deletedReport = await _dbContext.Report.FindAsync(report.Id);
        Assert.IsNull(deletedReport);
    }

    [TestMethod]
    public async Task DeleteByIdAsync_Should_Return_False_If_Report_Not_Found()
    {
        // Act
        var isDeleted = await _repository.DeleteByIdAsync(new(Guid.NewGuid()));

        // Assert
        Assert.IsFalse(isDeleted);
    }

    [TestMethod]
    public async Task GetAllAsync_Should_Return_All_Reports()
    {
        // Arrange
        Witness? testWitness = await _dbContext.Witness.FirstOrDefaultAsync();
        Report report1 = DataProvider.GetTestReports().FirstOrDefault()!;
        report1.WitnessId = testWitness!.Id;

        Report report2 = DataProvider.GetTestReports().FirstOrDefault()!;
        report2.WitnessId = testWitness!.Id;

        await _repository.AddAsync(report1);
        await _repository.AddAsync(report2);

        // Act
        List<Report>? reports = await _repository.GetAllAsync();

        // Assert
        Assert.AreEqual(2, reports.Count);
        Assert.IsTrue(reports.Any(r => r.Id == report1.Id));
        Assert.IsTrue(reports.Any(r => r.Id == report2.Id));
    }


    [TestMethod]
    public async Task GetByIdAsync_Should_Return_Report_By_Id()
    {
        // Arrange
        Witness? testWitness = await _dbContext.Witness.FirstOrDefaultAsync();
        Report report = DataProvider.GetTestReports().FirstOrDefault()!;
        report.WitnessId = testWitness!.Id;
        await _repository.AddAsync(report);

        // Act
        Report? retrievedReport = await _repository.GetByIdAsync(report.Id);

        // Assert
        Assert.IsNotNull(retrievedReport);
        Assert.AreEqual(report.Id, retrievedReport.Id);
    }

    [TestMethod]
    public async Task UpdateAsync_Should_Update_Report_In_Context()
    {
        // Arrange
        Witness? testWitness = await _dbContext.Witness.FirstOrDefaultAsync();
        Report report = DataProvider.GetTestReports().FirstOrDefault()!;
        report.WitnessId = testWitness!.Id;
        report = await _repository.AddAsync(report);

        Report? updatedReport = report;
        updatedReport.Title = "Updated Caption";
        updatedReport.Description = "Updated Description";

        // Act
        var updatedEntity = await _repository.UpdateAsync(updatedReport);

        // Assert
        Assert.AreEqual(updatedReport.Title, updatedEntity.Title);
        Assert.AreEqual(updatedReport.Description, updatedEntity.Description);
    }
}