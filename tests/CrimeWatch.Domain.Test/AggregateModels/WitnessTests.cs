namespace CrimeWatch.Domain.Test.AggregateModels;
[TestClass]
public class WitnessTests
{
    [TestMethod]
    public void WitnessCreate_ReturnsValidWitness()
    {
        // Arrange
        var firstName = "Alice";
        var lastName = "Johnson";
        var gender = Gender.Female;
        DateTime dateOfBirth = new(1995, 6, 15);
        var phoneNumber = "987-654-3210";
        var email = "alice@example.com";
        var password = "p@ssw0rd";

        // Act
        var witness = Witness.Create(firstName, lastName, gender, dateOfBirth, phoneNumber, email, password);

        // Assert
        Assert.IsNotNull(witness);
        Assert.AreEqual(firstName, witness.User?.FirstName);
        Assert.AreEqual(lastName, witness.User?.LastName);
        Assert.AreEqual(gender, witness.User?.Gender);
        Assert.AreEqual(dateOfBirth, witness.User?.DateOfBirth);
        Assert.AreEqual(phoneNumber, witness.User?.PhoneNumber);
        Assert.AreEqual(email, witness.Account?.Email);
        Assert.AreEqual(password, witness.Account?.Password);
    }
}
