namespace Domain.Test.AggregateModels.AccountAggregateTests;
[TestClass]
public class WhenCreatingAccount
{
    [TestMethod]
    public void Should_CreateAccountForWitness()
    {
        var nic = DataProvider.Nic;
        var firstName = DataProvider.FirstName;
        var lastName = DataProvider.LastName;
        var gender = DataProvider.Gender;
        var birthDate = DataProvider.BirthDate;
        var email = DataProvider.Email;
        var password = DataProvider.Password;
        var phoneNumber = DataProvider.PhoneNumber;

        var account =
            Account.CreateAccountForWitness(nic, firstName, lastName, gender, birthDate, email, password, phoneNumber);

        Assert.IsNotNull(account);

        Assert.IsNotNull(account.Id);
        Assert.AreEqual(AccountType.Witness, account.AccountType);
        Assert.AreEqual(email, account.Email);
        Assert.IsTrue(BCrypt.Net.BCrypt.Verify(password, account.Password));
        Assert.AreEqual(phoneNumber, account.PhoneNumber);
        Assert.IsNotNull(account.EmailVerificationCode);

        Assert.IsNotNull(account.Person);
        var person = account.Person;
        Assert.IsNotNull(person.Id);
        Assert.AreEqual(nic, person.Nic);
        Assert.AreEqual(firstName, person.FirstName);
        Assert.AreEqual(lastName, person.LastName);
        Assert.AreEqual(gender, person.Gender);
        Assert.AreEqual(birthDate, person.BirthDate);

        var witness = account.Witness;
        Assert.IsNotNull(witness);
        Assert.IsNotNull(witness.Id);
    }

    [TestMethod]
    public void ShouldRaise_AccountCreatedEvent_When_Creating_Account_For_Witness()
    {
        var nic = DataProvider.Nic;
        var firstName = DataProvider.FirstName;
        var lastName = DataProvider.LastName;
        var gender = DataProvider.Gender;
        var birthDate = DataProvider.BirthDate;
        var email = DataProvider.Email;
        var password = DataProvider.Password;
        var phoneNumber = DataProvider.PhoneNumber;

        var account =
            Account.CreateAccountForWitness(nic, firstName, lastName, gender, birthDate, email, password, phoneNumber);

        Assert.IsTrue(account.HasDomainEvent<AccountCreatedEvent>());
    }

    [TestMethod]
    public void Should_CreateAccountForModerator()
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

        var account = Account.CreateAccountForModerator(nic, firstName, lastName, gender, birthDate, policeId, city,
            province, email, password, phoneNumber);

        Assert.IsNotNull(account);

        Assert.IsNotNull(account.Id);
        Assert.AreEqual(AccountType.Moderator, account.AccountType);
        Assert.AreEqual(email, account.Email);
        Assert.IsTrue(BCrypt.Net.BCrypt.Verify(password, account.Password));
        Assert.AreEqual(phoneNumber, account.PhoneNumber);
        Assert.IsNotNull(account.EmailVerificationCode);

        Assert.IsNotNull(account.Person);
        var person = account.Person;
        Assert.IsNotNull(person.Id);
        Assert.AreEqual(nic, person.Nic);
        Assert.AreEqual(firstName, person.FirstName);

        Assert.IsNotNull(account.Moderator);
        var moderator = account.Moderator;
        Assert.IsNotNull(moderator.Id);
        Assert.AreEqual(policeId, moderator.PoliceId);
        Assert.AreEqual(city, moderator.City);
        Assert.AreEqual(province, moderator.Province);
    }

    [TestMethod]
    public void ShouldRaise_AccountCreatedEvent_When_Creating_Account_For_Moderator()
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

        var account = Account.CreateAccountForModerator(nic, firstName, lastName, gender, birthDate, policeId, city,
            province, email, password, phoneNumber);

        Assert.IsTrue(account.HasDomainEvent<AccountCreatedEvent>());
    }
}
