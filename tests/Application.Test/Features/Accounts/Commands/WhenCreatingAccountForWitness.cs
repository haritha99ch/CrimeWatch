using Application.Features.Accounts.Commands.CreateAccountForWitness;

namespace Application.Test.Features.Accounts.Commands;
[TestClass]
public class WhenCreatingAccountForWitness : TestBase
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
    public async Task ShouldCreate_AccountForWitness()
    {
        var nic = DataProvider.Nic;
        var firstName = DataProvider.FirstName;
        var lastName = DataProvider.LastName;
        var gender = DataProvider.Gender;
        var birthDay = DataProvider.BirthDate;
        var email = DataProvider.Email;
        var password = DataProvider.Password;
        var phoneNumber = DataProvider.PhoneNumber;

        var command = new CreateAccountForWitnessCommand(
            nic,
            firstName,
            lastName,
            gender,
            birthDay.ToDateTime(TimeOnly.MinValue),
            email,
            password,
            phoneNumber);
        var result = await Mediator.Send(command);
        var account = result.GetValue();

        Assert.IsNotNull(account);
    }
}
