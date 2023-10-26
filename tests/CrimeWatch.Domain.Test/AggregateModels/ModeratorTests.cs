namespace CrimeWatch.Domain.Test.AggregateModels;
[TestClass]
public class ModeratorTests
{
    [TestMethod]
    public void ModeratorCreate_ReturnsValidModerator()
    {
        // Arrange
        var firstName = "Michael";
        var lastName = "Smith";
        var gender = Gender.Male;
        DateTime dateOfBirth = new(1988, 3, 25);
        var phoneNumber = "555-123-4567";
        var policeId = "P12345";
        var province = "California";
        var email = "moderator@example.com";
        var password = "secureP@ss";

        // Act
        var moderator = Moderator.Create(firstName, lastName, gender, dateOfBirth, phoneNumber, policeId, province,
            email, password)!;

        // Assert
        Assert.IsNotNull(moderator);
        Assert.AreEqual(firstName, moderator.User?.FirstName);
        Assert.AreEqual(lastName, moderator.User?.LastName);
        Assert.AreEqual(gender, moderator.User?.Gender);
        Assert.AreEqual(dateOfBirth, moderator.User?.DateOfBirth);
        Assert.AreEqual(phoneNumber, moderator.User?.PhoneNumber);
        Assert.AreEqual(email, moderator.Account?.Email);
        Assert.AreEqual(password, moderator.Account?.Password);
        Assert.AreEqual(policeId, moderator.PoliceId);
        Assert.AreEqual(province, moderator.Province);
    }
}
