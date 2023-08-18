using CrimeWatch.Domain.AggregateModels.ModeratorAggregate;

namespace CrimeWatch.Domain.Test.AggregateModels;
[TestClass]
public class ModeratorTests
{
    [TestMethod]
    public void ModeratorCreate_ReturnsValidModerator()
    {
        // Arrange
        string firstName = "Michael";
        string lastName = "Smith";
        Gender gender = Gender.Male;
        DateOnly dateOfBirth = new(1988, 3, 25);
        string phoneNumber = "555-123-4567";
        string policeId = "P12345";
        string province = "California";
        string email = "moderator@example.com";
        string password = "secureP@ss";

        // Act
        Moderator moderator = Moderator.Create(firstName, lastName, gender, dateOfBirth, phoneNumber, policeId, province, email, password);

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