using Domain.AggregateModels.AccountAggregate;
using Domain.AggregateModels.AccountAggregate.Enums;
using Domain.Test.Helpers;

namespace Domain.Test.AggregateModels;
[TestClass]
public class AccountAggregateTests
{
    [TestMethod]
    public void Create_Account_For_Witness()
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
        Assert.AreEqual(password, account.Password);
        Assert.AreEqual(phoneNumber, account.PhoneNumber);

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
    public void Create_Account_For_Moderator()
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
        Assert.AreEqual(password, account.Password);
        Assert.AreEqual(phoneNumber, account.PhoneNumber);

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
    public void Update_Witness_Account()
    {
        var account = DataProvider.TestAccountForWitness;
        var person = account.Person!;

        var newEmail = DataProvider.Email;
        var newLastName = DataProvider.LastName;

        account.UpdateWitness(person.Nic, person.FirstName, newLastName, person.Gender, person.BirthDate, newEmail,
            account.Password, account.PhoneNumber);

        Assert.IsNotNull(account.Person);
        person = account.Person;
        Assert.AreEqual(newEmail, account.Email);
        Assert.AreEqual(newLastName, person.LastName);

    }

    [TestMethod]
    public void Update_Moderator_Account()
    {
        var account = DataProvider.TestAccountForModerator;
        var person = account.Person!;
        var moderator = account.Moderator!;

        var newEmail = DataProvider.Email;
        var newLastName = DataProvider.LastName;

        account.UpdateModerator(person.Nic, person.FirstName, newLastName, person.Gender, person.BirthDate,
            moderator.PoliceId, moderator.City, moderator.Province, newEmail,
            account.Password, account.PhoneNumber);

        Assert.IsNotNull(account.Person);
        person = account.Person;
        Assert.AreEqual(newEmail, account.Email);
        Assert.AreEqual(newLastName, person.LastName);

        Assert.IsNotNull(account.Moderator);
        moderator = account.Moderator;
        Assert.AreEqual(moderator.City, moderator.City);
        Assert.AreEqual(moderator.Province, moderator.Province);
    }
}
