using Application.Features.Accounts.Commands.CreateAccountForModerator;

namespace Application.Test.Features.Accounts.Commands;

[TestClass]
public class WhenCreatingAccountForModerator : TestBase
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
    public async Task ShouldCreate_AccountForModerator()
    {
        var nic = DataProvider.Nic;
        var policeId = DataProvider.PoliceId;
        var city = DataProvider.City;
        var province = DataProvider.Province;
        var firstName = DataProvider.FirstName;
        var lastName = DataProvider.LastName;
        var gender = DataProvider.Gender;
        var birthDate = DataProvider.BirthDate;
        var email = DataProvider.Email;
        var password = DataProvider.Password;
        var phoneNumber = DataProvider.PhoneNumber;

        var command = new CreateAccountForModeratorCommand(
            nic,
            firstName,
            lastName,
            gender,
            birthDate.ToDateTime(TimeOnly.MinValue),
            policeId,
            city,
            province,
            email,
            password,
            phoneNumber
        );
        var result = await Mediator.Send(command);
        var handler = new CreateAccountForModeratorCommandHandler(null!);
        var account = result.GetValue();

        Assert.IsNotNull(account);
    }
}
