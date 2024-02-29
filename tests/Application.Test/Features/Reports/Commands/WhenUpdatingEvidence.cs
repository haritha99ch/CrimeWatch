using Application.Errors.Common;
using Application.Features.Reports.Commands.UpdateEvidence;

namespace Application.Test.Features.Reports.Commands;
[TestClass]
public class WhenUpdatingEvidence : TestBase
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
    public async Task ShouldReturn_UpdatedEvidenceDetails_When_User_Is_Authorized()
    {
        var currentUser = DataProvider.TestAccountForWitness;
        var report = DataProvider.GetReportWithAEvidence(currentUser.Id);
        await DbContext.Accounts.AddAsync(currentUser);
        await DbContext.Reports.AddAsync(report);
        await SaveAndClearChangeTrackerAsync();
        var updatedCaption = DataProvider.Caption;
        var updatedDescription = DataProvider.Description;
        var no = report.Location.No!;
        var street1 = report.Location.Street1;
        var updatedStreet2 = DataProvider.Street2;
        var city = report.Location.City;
        var province = report.Location.Province;
        var newMediaItems = Enumerable.Range(1, 3)
            .Select(_ => DataProvider.TestMediaItem)
            .ToList();
        var exitingMediaItems = report.Evidences[0].MediaItems;
        exitingMediaItems.RemoveAt(1);
        var expectingMediaItemCount = exitingMediaItems.Count + newMediaItems.Count;

        // Act
        GenerateTokenAndInvoke(currentUser);
        var reportId = report.Id;
        var evidenceId = report.Evidences[0].Id;

        var command = new UpdateEvidenceCommand(
            reportId,
            evidenceId,
            updatedCaption,
            updatedDescription,
            no,
            street1,
            updatedStreet2,
            city,
            province,
            exitingMediaItems,
            newMediaItems
        );
        var result = await Mediator.Send(command);

        // Assert
        var evidence = result.GetValue();
        Assert.AreEqual(updatedCaption, evidence.Caption);
        Assert.AreEqual(updatedDescription, evidence.Description);
        Assert.AreEqual(no, evidence.Location.No);
        Assert.AreEqual(street1, evidence.Location.Street1);
        Assert.AreEqual(updatedStreet2, evidence.Location.Street2);
        Assert.AreEqual(city, evidence.Location.City);
        Assert.AreEqual(province, evidence.Location.Province);
        Assert.AreEqual(expectingMediaItemCount, evidence.MediaItems.Count);
    }

    [TestMethod]
    public async Task ShouldReturn_UpdatedEvidenceDetails_When_User_Is_NotAuthorized()
    {
        var testUser = DataProvider.TestAccountForWitness;
        var currentUser = DataProvider.TestAccountForWitness;
        var report = DataProvider.GetReportWithAEvidence(testUser.Id);
        await DbContext.Accounts.AddRangeAsync([testUser, currentUser]);
        await DbContext.Reports.AddAsync(report);
        await SaveAndClearChangeTrackerAsync();
        var updatedCaption = DataProvider.Caption;
        var updatedDescription = DataProvider.Description;
        var no = report.Location.No!;
        var street1 = report.Location.Street1;
        var updatedStreet2 = DataProvider.Street2;
        var city = report.Location.City;
        var province = report.Location.Province;
        var newMediaItems = Enumerable.Range(1, 3)
            .Select(_ => DataProvider.TestMediaItem)
            .ToList();
        var exitingMediaItems = report.Evidences[0].MediaItems;

        // Act
        GenerateTokenAndInvoke(currentUser);
        var reportId = report.Id;
        var evidenceId = report.Evidences[0].Id;

        var command = new UpdateEvidenceCommand(
            reportId,
            evidenceId,
            updatedCaption,
            updatedDescription,
            no,
            street1,
            updatedStreet2,
            city,
            province,
            exitingMediaItems,
            newMediaItems
        );
        var result = await Mediator.Send(command);

        // Assert
        Assert.IsTrue(result.GetError().Is<UnauthorizedError>());
    }
}
