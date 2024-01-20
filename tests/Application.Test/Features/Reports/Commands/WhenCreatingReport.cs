using Application.Errors.Common;
using Application.Features.Reports.Commands.CreateReport;

namespace Application.Test.Features.Reports.Commands;
[TestClass]
public class WhenCreatingReport : TestBase
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
    public async Task Should_Create_Report_When_Authorized()
    {
        // Arrange
        var testAccount = DataProvider.TestAccountForModerator;
        await DbContext.Accounts.AddAsync(testAccount);
        await SaveAndClearChangeTrackerAsync();
        GenerateTokenAndInvoke(testAccount);
        var caption = DataProvider.Caption;
        var description = DataProvider.Description;
        var no = DataProvider.No;
        var street1 = DataProvider.Street1;
        var street2 = DataProvider.Street2;
        var city = DataProvider.City;
        var province = DataProvider.Province;
        var authorId = testAccount.Id;
        var mediaItemUpload = DataProvider.TestMediaItem;
        var violationTypes = DataProvider.ViolationTypes;

        // Act
        var command = new CreateReportCommand(
            authorId,
            caption,
            description,
            no,
            street1,
            street2,
            city,
            province,
            violationTypes,
            mediaItemUpload);
        var result = await Mediator.Send(command);
        var report = result.GetValue();

        // Assert
        Assert.IsNotNull(report);
        Assert.IsNotNull(report.MediaItem);
        Assert.AreEqual(authorId, report.AuthorId);
        Assert.AreEqual(caption, report.Caption);
        Assert.AreEqual(description, report.Description);
        Assert.AreEqual(no, report.Location.No);
        Assert.AreEqual(street1, report.Location.Street1);
        Assert.AreEqual(street2, report.Location.Street2);
        Assert.AreEqual(city, report.Location.City);
        Assert.AreEqual(province, report.Location.Province);
        Assert.AreEqual(violationTypes, report.ViolationTypes);
    }

    [TestMethod]
    public async Task Should_Return_UnauthorizedError_When_Authorization_Is_Invalid()
    {
        // Arrange
        var testAccount = DataProvider.TestAccountForModerator;
        var invalidAccount = DataProvider.TestAccountForModerator;
        await DbContext.Accounts.AddRangeAsync([testAccount, invalidAccount]);
        await SaveAndClearChangeTrackerAsync();
        GenerateTokenAndInvoke(invalidAccount);
        var caption = DataProvider.Caption;
        var description = DataProvider.Description;
        var no = DataProvider.No;
        var street1 = DataProvider.Street1;
        var street2 = DataProvider.Street2;
        var city = DataProvider.City;
        var province = DataProvider.Province;
        var authorId = testAccount.Id;
        var mediaItemUpload = DataProvider.TestMediaItem;
        var violationTypes = DataProvider.ViolationTypes;

        // Act
        var command = new CreateReportCommand(
            authorId,
            caption,
            description,
            no,
            street1,
            street2,
            city,
            province,
            violationTypes,
            mediaItemUpload);
        var result = await Mediator.Send(command);
        var error = result.GetError();

        // Assert
        Assert.IsTrue(error.Is<UnauthorizedError>());
    }
}
