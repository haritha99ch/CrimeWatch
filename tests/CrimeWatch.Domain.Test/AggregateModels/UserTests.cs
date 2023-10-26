namespace CrimeWatch.Domain.Test.AggregateModels;
[TestClass]
public class UserTests
{
    [TestMethod]
    public void UserCreate_ReturnsValidUser()
    {
        // Arrange
        var firstName = "John";
        var lastName = "Doe";
        var gender = Gender.Male;
        DateTime dateOfBirth = new(1990, 1, 1);
        var phoneNumber = "123-456-7890";

        // Act
        var user = User.Create(firstName, lastName, gender, dateOfBirth, phoneNumber);

        // Assert
        Assert.IsNotNull(user);
        Assert.AreEqual(firstName, user.FirstName);
        Assert.AreEqual(lastName, user.LastName);
        Assert.AreEqual(gender, user.Gender);
        Assert.AreEqual(dateOfBirth, user.DateOfBirth);
        Assert.AreEqual(phoneNumber, user.PhoneNumber);
    }
}
