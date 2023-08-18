using CrimeWatch.Domain.AggregateModels.WitnessAggregate;

namespace CrimeWatch.Domain.Test.AggregateModels;
[TestClass]
public class WitnessTests
{
    [TestMethod]
    public void WitnessCreate_ReturnsValidWitness()
    {
        // Arrange
        string firstName = "Alice";
        string lastName = "Johnson";
        Gender gender = Gender.Female;
        DateOnly dateOfBirth = new(1995, 6, 15);
        string phoneNumber = "987-654-3210";
        string email = "alice@example.com";
        string password = "p@ssw0rd";

        // Act
        Witness witness = Witness.Create(firstName, lastName, gender, dateOfBirth, phoneNumber, email, password);

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